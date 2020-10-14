using Microsoft.EntityFrameworkCore;
using Payments.Storage.Configurations;

namespace Payments.Storage
{
    public class DataContext : DbContext
    {
        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        public DbSet<CurrencyCode> CurrencyCodes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyCodeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new MerchantConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}