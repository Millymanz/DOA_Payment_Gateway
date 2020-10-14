using Payments.Banking.Enum;

namespace Payments.Banking
{
    public class PayOrderDetail
    {
        public string OrderId { get; }
        public InternalPaymentStatus Outcome { get; }

        public PayOrderDetail(string orderUniqueIdentifier, InternalPaymentStatus status)
        {
            OrderId = orderUniqueIdentifier;
            Outcome = status;
        }
    }
}