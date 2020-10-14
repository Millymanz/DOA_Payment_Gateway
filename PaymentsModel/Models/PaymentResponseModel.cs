using System;

namespace Payments.DTO.Models
{
    public class PaymentResponseModel
    {
        public bool PaymentSuccess { get; set; }
        public Guid PaymentId { get; set; }
    }
}