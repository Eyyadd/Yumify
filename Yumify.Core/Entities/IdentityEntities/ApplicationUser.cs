using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public Address Address { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
    }
}
