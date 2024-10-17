using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Repository.Data.Configuration
{
    internal class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.Property(OI => OI.Price)
                .HasColumnType("decimal(12,2)");
        }
    }
}
