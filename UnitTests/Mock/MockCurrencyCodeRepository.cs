using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payments;
using Payments.Storage.Repositories.Interfaces;

namespace UnitTests.Mock
{
    public class MockCurrencyCodeRepository : ICurrencyCodeRepository
    {
        private IEnumerable<CurrencyCode> _currencyCodes;

        public MockCurrencyCodeRepository(IEnumerable<CurrencyCode> currencyCodes)
        {
            _currencyCodes = currencyCodes;
        }

        public async Task<IEnumerable<CurrencyCode>> GetAllAsync()
        {
            return _currencyCodes;
        }

        public Task<CurrencyCode> GetByCurrencyCodedAsync(string currencyCode)
        {
            return Queryable.AsQueryable(_currencyCodes).SingleOrDefaultAsync(p => p.Code == currencyCode);
        }

        public bool IsValid(string currencyCode)
        {
           return _currencyCodes.Any(p => p.Code == currencyCode);
        }

        //do nothing
        public Task SaveChangesAsync()
        {
            return null;
        }
    }
}