using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities.IdentityEntities;

namespace Yumify.Core.IServices
{
    public interface IAuthSerivce
    {
        Task<string> CreateToken(ApplicationUser applicationUser);
    }
}
