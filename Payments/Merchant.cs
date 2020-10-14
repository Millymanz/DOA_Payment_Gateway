
using System;
using System.ComponentModel.DataAnnotations;
using Payments.Banking.Interfaces;

namespace Payments
{
    public class Merchant : IMerchant
    {
        private Merchant()
        {
            IsDisabled = false;
        }

        public Guid MerchantId { get; private set; }        
        public bool IsDisabled { get; private set; }
        public string MerchantName { get; private set; }
        public string ApiKey { get; private set; }
        

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public static Merchant Register(string merchantName, CreditCardInfo creditCardInfo, IRandomGenerator randomGen)
        {
            var newMerchant = new Merchant
            {
                MerchantName = merchantName
            };

            newMerchant.GenerateApiKey(randomGen);

            return newMerchant;
        }

        private void GenerateApiKey(IRandomGenerator randomGen)
        {
            const int apiKeyLength = 22;
            ApiKey = randomGen.GenerateRandomAlphanumericString(apiKeyLength);
        }
        
        public void Disable()
        {
            IsDisabled = true;
        }

        public void Enable()
        {
            IsDisabled = false;
        }      
    }
}