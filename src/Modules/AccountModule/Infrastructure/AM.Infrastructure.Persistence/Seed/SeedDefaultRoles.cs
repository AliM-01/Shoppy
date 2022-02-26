using AM.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AM.Infrastructure.Persistence.Seed;

public static class SeedDefaultRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles

        if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        }

        if (!await roleManager.RoleExistsAsync(Roles.BasicUser.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));
        }
    }
}

