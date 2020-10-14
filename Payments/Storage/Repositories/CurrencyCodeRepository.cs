using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payments.Storage.Repositories.Interfaces;

namespace Payments.Storage.Repositories
{
    public class CurrencyCodeRepository : ICurrencyCodeRepository
    {
        public CurrencyCodeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private readonly DataContext _dataContext;

        public async Task<IEnumerable<CurrencyCode>> GetAllAsync()
        {
            return await _dataContext.CurrencyCodes.ToListAsync().ConfigureAwait(false);
        }

        public Task<CurrencyCode> GetByCurrencyCodedAsync(string currencyCode)
        {
            return _dataContext.CurrencyCodes.SingleOrDefaultAsync(p => p.Code == currencyCode);
        }

        public bool IsValid(string currencyCode)
        {
            return _dataContext.CurrencyCodes.AnyAsync(p => p.Code == currencyCode).Result;
        }

        public Task SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }
    }
}