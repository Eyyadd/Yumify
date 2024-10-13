using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.IdentityEntities;

namespace Yumify.Repository.IDentity
{
    public class IdentityYumifyDbContext : IdentityDbContext<ApplicationUser>
    {

        public IdentityYumifyDbContext(DbContextOptions<IdentityYumifyDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Address> Addresses { get; set; }
    }
}
