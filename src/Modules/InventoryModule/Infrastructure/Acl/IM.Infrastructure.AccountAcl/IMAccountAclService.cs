using AM.Domain.Account;
using AM.Infrastructure.RepositoryExtensions;
using Ardalis.GuardClauses;
using IM.Application.Sevices;
using Microsoft.AspNetCore.Identity;

namespace IM.Infrastructure.AccountAcl;
public class IMAccountAclService : IIMAccountAclService
{
    #region ctor

    private readonly UserManager<Account> _userManager;

    public IMAccountAclService(UserManager<Account> userManager)
    {
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion

    public async Task<string> GetFullName(string userId)
    {
        return await _userManager.GetFullName(userId);
    }
}
