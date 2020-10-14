using System;
using Payments;
using Payments.DTO.Models;

namespace PaymentGateway.Helpers
{
    /// <summary>
    /// NOTE: Possible improvement: Could be superseded by a mapping framework like AutoMapper.
    /// </summary>
    public class PaymentUtility
    {    
        public static string MaskCreditCardNo(string cardNo)
        {
            if (cardNo.Length < 4)
            {
                throw new InvalidOperationException("Invalid credit card");
            }
                        
            var maskLen = cardNo.Length - 4;
            char maskCharacter = '*';

            var maskedPart = new string(maskCharacter, maskLen);
            var unmaskedPart = cardNo.Substring(maskLen);
            return maskedPart + unmaskedPart;
        }

        public static PaymentStatusCode ConvertPaymentState(PaymentState paymentState)
        {
            switch (paymentState)
            {
                case PaymentState.Failed:
                    return PaymentStatusCode.PaymentFailure;
                case PaymentState.Success:
                    return PaymentStatusCode.PaymentSuccess;

                case PaymentState.Created:
                    throw new InvalidOperationException("Created state is an in-between state and should never " +
                        "result in a customer-accessible state");
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentState), paymentState, null);
            }
        }

    }
}