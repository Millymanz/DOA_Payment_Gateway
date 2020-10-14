using System.Threading.Tasks;

namespace Payments.Banking.Interfaces
{ 
    public interface IBankService
    {
        /// <summary>
        /// Sends the payment order to the Bank to do the actual retrieval of money from the Shopper's card and payout to the Merchant.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currencyCode"></param>
        /// <param name="sourceCreditCardInformation"></param>
        /// <returns></returns>
        Task<PayOrderDetail> PayAsync(decimal amount, string currencyCode, CreditCardInfo sourceCreditCardInformation);
                                                           
    }
}