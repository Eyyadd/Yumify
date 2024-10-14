using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yumify.Core.Entities.IdentityEntities;

namespace Yumify.Service.Helper.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser?> GetUserAddressByEmail(this UserManager<ApplicationUser> _userManager,string mail)
        {
            var user= await _userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email==mail);
            return user;
        }
    }
}
