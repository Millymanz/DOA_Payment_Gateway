using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payments;
using Payments.DTO;
using Swashbuckle.AspNetCore.Annotations;
using Payments.Storage.Repositories.Interfaces;
using Payments.Banking.Interfaces;
using AutoMapper;
using Payments.DTO.Models;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController(ILogger<PaymentsController> logger, ICurrencyCodeRepository currencyRepository, IPaymentRepository paymentRepository, IMerchantRepository merchantRepository,
                                  IBankService bankService, IMapper mapper)
        {
            _logger = logger;
            _paymentRepository = paymentRepository;
            _merchantRepository = merchantRepository;
            _currencyCodeRepository = currencyRepository;

            _bankService = bankService;
            _mapper = mapper;
        }

        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ICurrencyCodeRepository _currencyCodeRepository;
        private readonly IBankService _bankService;

        private readonly IMapper _mapper;


        [HttpPost("pay")]
        [SwaggerOperation("Process a payment through the payment gateway and receive either a successful or unsuccessful response")]
        [SwaggerResponse(200, "The payment request generated and forwarded to bank", typeof(PaymentResponseModel))]
        [SwaggerResponse(404, "The payment request not valid", typeof(ErrorResponseModel))]
        public async Task<ActionResult> Pay([FromBody]PaymentRequestModel requestModel)
        {
            var merchant = ValidateMerchantCall();

            if (merchant == null)
            {
                return BadRequest(new ErrorResponseModel("Invalid API credentials check header"));
            }

            var creditCardInformation = new CreditCardInfo(requestModel.CreditCardNo,
                new ExpiryDate(requestModel.CreditCardExpiryMonth, requestModel.CreditCardExpiryYear), requestModel.CreditCardCVV);

            var paymentAmount = new PaymentAmount(requestModel.Amount, requestModel.CurrencyCode);
            var payment = Payment.Create(paymentAmount, creditCardInformation, merchant.MerchantId, requestModel.ShopperId);

            bool success = await payment.TryPay(_bankService, _currencyCodeRepository);

            _paymentRepository.Add(payment);

            await _paymentRepository.SaveChangesAsync();

            _logger.LogInformation($"New payment entry: {payment.PaymentId}");

            var response= new PaymentResponseModel
            {                
                PaymentId = payment.PaymentId,
                PaymentSuccess = success
            };

            return Accepted(response);
        }

        [HttpGet("{paymentId}")]
        [SwaggerOperation("Retrieve the details of a previously made payment")]
        [SwaggerResponse(200, "Payment details successfully retrieved", typeof(GetPaymentResponse))]
        [SwaggerResponse(404, "Payment details not found", typeof(ErrorResponseModel))]
        public async Task<ActionResult> Get([FromRoute]GetPaymentRequest requestModel)
        {
            var merchant = ValidateMerchantCall();

            if (merchant == null)
            {
                return BadRequest(new ErrorResponseModel("Invalid API credentials check header"));
            }

            _logger.LogInformation(requestModel.PaymentId.ToString());

            var payment = await _paymentRepository.GetByPaymentIdAsync(requestModel.PaymentId);

            if (payment == null)
            {
                return NotFound();
            }

            if (payment.MerchantId != merchant.MerchantId)
            {
                return BadRequest(new ErrorResponseModel("Access restricted!"));
            }

            var response = new GetPaymentResponse
            {
                Payment = _mapper.Map<PaymentModel>(payment)
            };

            return Ok(response);
        }

        /// <summary>
        /// Calls are checked via the headers to ensure that the merchant is valid, 
        /// this is a form of basic authentication.
        ///  
        /// </summary>
        /// <returns>The valid Merchant</returns>
        private Merchant ValidateMerchantCall()
        {
            Merchant merchant = null;

            if (Request.Headers.ContainsKey(AppConstants.HeaderName) && Request.Headers.ContainsKey(AppConstants.ApiKey))
            {
                var rawMerchantId = Request.Headers[AppConstants.HeaderName].First();
                var rawApiKey = Request.Headers[AppConstants.ApiKey].First();

                if (Guid.TryParse(rawMerchantId, out var parsedMerchantId))
                {
                    var merchantTemp = _merchantRepository.GetByMerchantId(parsedMerchantId);

                    if (merchantTemp != null && rawApiKey == merchantTemp.ApiKey)
                    {
                        merchant = merchantTemp;
                    }
                }
            }

            return merchant;
        }
    }
}