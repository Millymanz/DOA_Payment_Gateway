using System;

namespace Payments
{
    public class ExpiryDate
    {
        public ExpiryDate(int month, int year)
        {
            Month = month;
            Year = year;
        }

        #region  EntityFramework
        [Obsolete("Needed for Entity Framework", true)]
        private ExpiryDate()
        {

        }
        #endregion

        public int Month { get; }
        public int Year { get; }
    }
}