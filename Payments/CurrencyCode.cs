using System;
using System.ComponentModel.DataAnnotations;

namespace Payments
{
    public class CurrencyCode
    {
        private CurrencyCode()
        {
          
        }

        public CurrencyCode(string code, string currency)
        {
            Code = code;
            Currency = currency;
        }

        public Guid CodeId { get; private set; }

        public string Code { get; private set; }
        public string Currency { get; private set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
     
    }
}