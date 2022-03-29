using _0_Framework.Application.Exceptions;
using AM.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AM.Infrastructure.Shared.RepositoryExtensions;
public static class UserManagerExtension
{
    public static async Task<string> GetFullName(this UserManager<Account> userManager, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new NotFoundApiException("کاربری با این شناسه پیدا نشد");

        return $"{user.FirstName} {user.LastName}";
    }
}
