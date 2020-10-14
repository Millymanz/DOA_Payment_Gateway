using System;

namespace Payments.DTO.Models
{
    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string ShopperId { get; set; }
        public int CreditCardCVV { get; set; }
        public string CreditCardNo { get; set; }
        public int CreditCardExpiryMonth { get; set; }
        public int CreditCardExpiryYear { get; set; }        
    }
}