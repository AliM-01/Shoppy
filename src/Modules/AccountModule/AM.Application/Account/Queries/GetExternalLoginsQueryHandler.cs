using Microsoft.AspNetCore.Authentication;

namespace AM.Application.Account.Queries;

public record GetExternalLoginsQuery() : IRequest<IEnumerable<AuthenticationScheme>>;

public class GetExternalLoginsQueryHandler : IRequestHandler<GetExternalLoginsQuery, IEnumerable<AuthenticationScheme>>
{
    private readonly SignInManager<Domain.Account.Account> _signInManager;

    public GetExternalLoginsQueryHandler(SignInManager<Domain.Account.Account> signInManager)
    {
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
    }

    public async Task<IEnumerable<AuthenticationScheme>> Handle(GetExternalLoginsQuery request, CancellationToken cancellationToken)
    {
        return await _signInManager.GetExternalAuthenticationSchemesAsync();
    }
}
