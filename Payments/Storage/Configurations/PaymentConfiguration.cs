using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Payments.Storage.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.State)
                   .HasMaxLength(200)
                   .HasConversion(new EnumToStringConverter<PaymentState>());

            builder.Property(p => p.ShopperId)
                   .HasMaxLength(200);


            builder.OwnsOne(p => p.PaymentAmount, paBuilder =>
            {
                paBuilder.Property(pa => pa.Amount);
                paBuilder.Property(pa => pa.CurrencyCode).HasMaxLength(5);
            });

            builder.OwnsOne(p => p.CreditCardInfo, cciBuilder =>
            {
                cciBuilder.Property(cci => cci.CardNo).HasMaxLength(100);
                cciBuilder.OwnsOne(cci => cci.ExpiryDate, expiryDateBuilder =>
                {
                    expiryDateBuilder.Property(ed => ed.Month);
                    expiryDateBuilder.Property(ed => ed.Year);
                });
                cciBuilder.Property(cci => cci.CVV).HasMaxLength(5);
            });

            builder.Property(p => p.Timestamp);
        }
    }
}