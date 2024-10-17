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
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.OrderStatus)
                .HasConversion(
                status => status.ToString(),
                status =>(OrderStatus) Enum.Parse(typeof(OrderStatus), status)
                );

            builder.OwnsOne(O => O.ShippingAddress, address => address.WithOwner());

            builder.Property(O => O.SubtotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany(D=>D.Orders)
                .HasForeignKey(Fk=>Fk.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
