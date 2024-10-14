using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Yumify.Core.Entities.IdentityEntities;

namespace Yumify.Service.Helper.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser?> GetUserAddressByEmail(this UserManager<ApplicationUser> _userManager,ClaimsPrincipal user)
        {
            var Mail= user.FindFirstValue(ClaimTypes.Email);
            var UserWithAddress= await _userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email== Mail);
            return UserWithAddress;
        }
    }
}
