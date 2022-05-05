using AM.Application.Contracts.Account.Queries;
using Microsoft.AspNetCore.Authentication;

namespace AM.Application.Account.QueryHandles;

public class GetExternalLoginsQueryHandler : IRequestHandler<GetExternalLoginsQuery, ApiResult<IEnumerable<AuthenticationScheme>>>
{
    #region Ctor

    private readonly SignInManager<Domain.Account.Account> _signInManager;

    public GetExternalLoginsQueryHandler(SignInManager<Domain.Account.Account> signInManager)
    {
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
    }

    #endregion Ctor

    public async Task<ApiResult<IEnumerable<AuthenticationScheme>>> Handle(GetExternalLoginsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var externalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync();

        return ApiResponse.Success<IEnumerable<AuthenticationScheme>>(externalLogins);
    }
}
