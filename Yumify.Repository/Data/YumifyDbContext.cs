﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Repository.Data
{
    public class YumifyDbContext:DbContext
    {
        public YumifyDbContext(DbContextOptions<YumifyDbContext> dbContextOptions):base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
