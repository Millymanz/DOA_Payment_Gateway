using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Payments.Storage.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment> GetByPaymentIdAsync(Guid paymentId);

        void Add(Payment payment);

        /// <summary>
        /// Persist changed data to durable storage
        /// </summary>
        Task SaveChangesAsync();
    }
}