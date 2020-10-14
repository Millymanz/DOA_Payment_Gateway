using System;

namespace Payments
{

    public class CreditCardInfo
    {
        public CreditCardInfo(string cardNo, ExpiryDate expiryDate, int cvv)
        {
            if (string.IsNullOrWhiteSpace(cardNo))
                throw new ArgumentOutOfRangeException(nameof(cardNo));
            if (expiryDate == null)
                throw new ArgumentNullException(nameof(expiryDate));
            if (cvv <= 0)
                throw new ArgumentOutOfRangeException(nameof(cvv));

            CardNo = cardNo;
            
            CVV = cvv;
            ExpiryDate = expiryDate;
        }

        #region  EntityFramework
        [Obsolete("Needed for Entity Framework", true)]
        private CreditCardInfo()
        {

        }
        #endregion

        public string CardNo { get; }
        public ExpiryDate ExpiryDate { get; }
        public int CVV { get; }
    }
}