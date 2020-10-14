using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Payments.Banking;
using Payments.Banking.Enum;
using Payments.Banking.Interfaces;
using Payments.Storage.Repositories.Interfaces;

namespace Payments
{
    public class Payment
    {
        private Payment()
        {
            CreateDate = DateTime.UtcNow;
            State = PaymentState.Created;
        }

        public Guid PaymentId { get; private set; }
        public PaymentState State { get; private set; }

        public DateTime CreateDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }

        public PaymentAmount PaymentAmount { get; private set; }
        public CreditCardInfo CreditCardInfo { get; private set; }

        public string PaymentOrderId { get; private set; }

        public Guid MerchantId { get; private set; }
        public string ShopperId { get; private set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public static Payment Create(PaymentAmount amount, CreditCardInfo creditCardInfo, Guid merchantId, string shopperId)
        {
            return new Payment
            {
                PaymentAmount = amount,
                CreditCardInfo = creditCardInfo,
                MerchantId = merchantId,
                ShopperId = shopperId
            };
        }

        public async Task<bool> TryPay(IBankService bankService, ICurrencyCodeRepository currencyCodeRepository)
        {
            var outcome = false;

            if (SoftValidation(bankService, currencyCodeRepository, CreditCardInfo))
            {
                var res = await bankService.PayAsync(PaymentAmount.Amount, PaymentAmount.CurrencyCode, CreditCardInfo);

                PaymentOrderId = res.OrderId;

                switch (res.Outcome)
                {
                    case InternalPaymentStatus.Success:
                        {
                            State = PaymentState.Success;
                            PaymentDate = DateTime.UtcNow;
                            outcome = true;
                        }
                        break;
                    case InternalPaymentStatus.Failure_Rejected:
                    case InternalPaymentStatus.Failure_Unknown:
                        {
                            State = PaymentState.Failed;
                        }
                        break;
                    default:
                        {
                            State = PaymentState.Failed;
                        }
                        break;
                }
            }
            else
            {
                State = PaymentState.Failed;
            }

            return outcome;
        }

        private bool SoftValidation(IBankService bankService, ICurrencyCodeRepository currencyCodeRepository, CreditCardInfo merchantCreditCardInfo)
        {
            if (bankService == null)
            {
                return false;
            }
            else if (merchantCreditCardInfo == null)
            {
                return false;
            }
            else if (!(merchantCreditCardInfo.CVV.ToString().Length >= 3 && merchantCreditCardInfo.CVV.ToString().Length <= 4))
            {
                return false;
            }
            else if (!(merchantCreditCardInfo.CardNo.ToString().Length >= 12 && merchantCreditCardInfo.CardNo.ToString().Length <= 20))
            {
                return false;
            }
            else if ((merchantCreditCardInfo.ExpiryDate.Year < DateTime.Now.Year) || (merchantCreditCardInfo.ExpiryDate.Year < DateTime.Now.Year))
            {
                return false;
            }
            else if (!(merchantCreditCardInfo.ExpiryDate.Month >= 1 && merchantCreditCardInfo.ExpiryDate.Month <= 12))
            {
                return false;
            }
            else if (!(PaymentAmount.Amount >= 0 && merchantCreditCardInfo.ExpiryDate.Month <= 1000000000))
            {
                return false;
            }
            else if (!currencyCodeRepository.IsValid(PaymentAmount.CurrencyCode))
            {
                return false;
            }

            return true;
        }
    }
}