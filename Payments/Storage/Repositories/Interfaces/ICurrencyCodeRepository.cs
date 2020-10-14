using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Payments.Storage.Repositories.Interfaces
{
    public interface ICurrencyCodeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CurrencyCode>> GetAllAsync();

        Task<CurrencyCode> GetByCurrencyCodedAsync(string currencyCode);
        
        bool IsValid(string currencyCode);



        /// <summary>
        /// Persist changed data to storage
        /// </summary>
        Task SaveChangesAsync();
    }
}