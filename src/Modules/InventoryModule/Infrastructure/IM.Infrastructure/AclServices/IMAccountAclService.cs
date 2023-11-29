using AM.Domain.Account;
using AM.Infrastructure.RepositoryExtensions;
using Ardalis.GuardClauses;
using IM.Application.Sevices;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IM.Infrastructure.AclServices;

public class IMAccountAclService : IIMAccountAclService
{
    private readonly UserManager<Account> _userManager;

    public IMAccountAclService(UserManager<Account> userManager)
    {
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<string> GetFullName(string userId)
    {
        return await _userManager.GetFullName(userId);
    }
}
