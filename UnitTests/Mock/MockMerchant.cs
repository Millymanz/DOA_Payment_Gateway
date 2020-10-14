
using System;
using Payments;

namespace UnitTests.Mock
{
    public class MockMerchant : IMerchant
    {
        public MockMerchant()
        {
            IsDisable = false;
        }

        public Guid MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string ApiKey { get; set; }

        private bool IsDisable;

        public CreditCardInfo CreditCardInfo { get; set; }

        public byte[] RowVersion { get; set; }

        public void Enable()
        {
            IsDisable = false;
        }

        public void Disable()
        {
            IsDisable = true;
        }
    }
}