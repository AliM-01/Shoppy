using _0_Framework.Application.Extensions;
using _0_Framework.Infrastructure;
using AM.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AM.Infrastructure.Seed;

public static class SeedDefaultUsers
{
    public static async Task SeedAdminAsync(UserManager<Domain.Account.Account> userManager)
    {
        var adminUser = new Domain.Account.Account
        {
            Id = Guid.Parse(SeedUserIdConstants.AdminUser),
            UserName = Generator.UserName(),
            Email = "ali@gmail.com",
            FirstName = "ادمین",
            LastName = "ادمینی",
            PhoneNumber = "09123456789",
            EmailConfirmed = true
        };
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(adminUser, "123Pa$$word!");
            await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
            await userManager.AddToRoleAsync(adminUser, Roles.BasicUser.ToString());
        }
    }

    public static async Task SeedBasicUserAsync(UserManager<Domain.Account.Account> userManager)
    {
        var defaultUser = new Domain.Account.Account
        {
            Id = Guid.Parse(SeedUserIdConstants.BasicUser),
            UserName = Generator.UserName(),
            Email = "user1@g.com",
            FirstName = "کاربر",
            LastName = "کاربری",
            PhoneNumber = "09223456789",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(defaultUser, "123Pa$$word!");
            await userManager.AddToRoleAsync(defaultUser, Roles.BasicUser.ToString());
        }
    }
}
