using System;

namespace Payments
{
    public class PaymentAmount
    {
        public PaymentAmount(decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        #region  EntityFramework
        [Obsolete("Needed for Entity Framework", true)]
        private PaymentAmount()
        {

        }
        #endregion

        public decimal Amount { get; }
        public string CurrencyCode { get; }
    }
}