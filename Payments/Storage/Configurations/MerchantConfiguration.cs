using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payments.Storage.Configurations
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(m => m.MerchantId);

            builder.Property(m => m.MerchantName).HasMaxLength(200);
            builder.Property(m => m.ApiKey).HasMaxLength(100);

            builder.Property(m => m.Timestamp);
        }
    }
}