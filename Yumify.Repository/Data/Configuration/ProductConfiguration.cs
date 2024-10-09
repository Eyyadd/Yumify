using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;

namespace Yumify.Repository.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);

            builder.Property(P => P.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
