using System;

namespace Payments.DTO.Models
{
    public class CreditCardModel
    {
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public int ExpiryDateMonth { get; set; }
        public int ExpiryDateYear { get; set; }
        
    }
}