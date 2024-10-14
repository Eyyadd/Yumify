using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yumify.Core.Entities.IdentityEntities;

namespace Yumify.Repository.IDentity.DataSeeding
{
    public static class YumifyIdentitySeeding
    {
        public static async Task IdentitySeeding(UserManager<ApplicationUser> userManager, IdentityYumifyDbContext identityYumifyDbContext)
        {
            if (!userManager.Users.Any())
            {
                var jsonData = File.ReadAllText("../Yumify.Repository/IDentity/DataSeeding/Users.json");
                var serializedData = JsonSerializer.Deserialize<List<ApplicationUser>>(jsonData);
                if (serializedData?.Count() > 0)
                {
                    foreach (var user in serializedData)
                    {
                        await userManager.CreateAsync(user, user.PasswordHash!);
                    }
                }
            }
            if (!identityYumifyDbContext.Addresses.Any())
            {
                var jsonData = File.ReadAllText("../Yumify.Repository/IDentity/DataSeeding/Address.json");
                var serializedData = JsonSerializer.Deserialize<List<Address>>(jsonData);
                if (serializedData?.Count() > 0)
                {
                    foreach (var address in serializedData)
                    {
                        var CheckCreate = await identityYumifyDbContext.Addresses.AddAsync(address);
                        var Written = await identityYumifyDbContext.SaveChangesAsync();

                    }
                }
            }

        }
    }
}
