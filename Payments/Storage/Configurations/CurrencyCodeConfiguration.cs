using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Payments.Storage.Configurations
{
    public class CurrencyCodeConfiguration : IEntityTypeConfiguration<CurrencyCode>
    {
        public void Configure(EntityTypeBuilder<CurrencyCode> builder)
        {
            builder.HasKey(c => c.CodeId);

            builder.Property(c => c.Code).HasMaxLength(5);

            builder.Property(c => c.Currency).HasMaxLength(300);

            builder.Property(c => c.Timestamp);
        }
    }
}