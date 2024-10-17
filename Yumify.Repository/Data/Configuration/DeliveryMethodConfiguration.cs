using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Repository.Data.Configuration
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.Cost)
                .HasColumnType("decimal(12,2)");
            
        }
    }
}
