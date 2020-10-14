using Payments.Banking.Enum;
using Payments.Banking.Interfaces;
using System;
using System.Threading.Tasks;

namespace Payments.Banking
{
    public class BankServiceMock : IBankService
    {
        private double _threshold;

        public BankServiceMock()
        {
            _threshold = 0.73;
        }

        public BankServiceMock(double threshold)
        {
            _threshold = threshold;
        }

        private readonly Random _random = new Random();

        public Task<PayOrderDetail> PayAsync(decimal amount, string currencyCode, CreditCardInfo sourceCreditCardInformation)
        {
            var success = SimulateSuccessOrFailure();

            var resultStatus = success ? InternalPaymentStatus.Success : InternalPaymentStatus.Failure_Rejected;

            var result = new PayOrderDetail(Guid.NewGuid().ToString(), resultStatus);

            return Task.FromResult(result);
        }

        private bool SimulateSuccessOrFailure()
        {
            var randomizedOrderSuccessValue = _random.NextDouble();

            if (randomizedOrderSuccessValue < _threshold)
            {
                return true;
            }

            return false;
        }
    }
}