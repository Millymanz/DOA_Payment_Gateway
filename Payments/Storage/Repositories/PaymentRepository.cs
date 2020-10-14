using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payments.Storage.Repositories.Interfaces;

namespace Payments.Storage.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private readonly DataContext _dataContext;

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _dataContext.Payments.ToListAsync().ConfigureAwait(false);
        }

        public Task<Payment> GetByPaymentIdAsync(Guid paymentId)
        {
            return _dataContext.Payments.SingleOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public void Add(Payment payment)
        {
            _dataContext.Payments.Add(payment);
        }

        public Task SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }
    }
}