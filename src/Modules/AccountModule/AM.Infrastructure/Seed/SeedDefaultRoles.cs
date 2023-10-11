using AM.Domain.Account;
using AM.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AM.Infrastructure.Seed;

public static class SeedDefaultRoles
{
    public static async Task SeedAsync(RoleManager<AccountRole> roleManager)
    {
        //Seed Roles

        if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        {
            await roleManager.CreateAsync(new AccountRole
            {
                Name = Roles.Admin.ToString(),
                NormalizedName = Roles.Admin.ToString().ToUpper(),
            });
        }

        if (!await roleManager.RoleExistsAsync(Roles.BasicUser.ToString()))
        {
            await roleManager.CreateAsync(new AccountRole
            {
                Name = Roles.BasicUser.ToString(),
                NormalizedName = Roles.BasicUser.ToString().ToUpper(),
            });
        }
    }
}

