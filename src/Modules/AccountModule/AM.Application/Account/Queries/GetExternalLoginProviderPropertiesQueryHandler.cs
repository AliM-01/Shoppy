using Microsoft.AspNetCore.Authentication;

namespace AM.Application.Account.Queries;

public record GetExternalLoginProviderPropertiesQuery(string Provider, string ReturnUrl) : IRequest<AuthenticationProperties>;

public class GetExternalLoginProviderPropertiesQueryHandler : IRequestHandler<GetExternalLoginProviderPropertiesQuery, AuthenticationProperties>
{
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public GetExternalLoginProviderPropertiesQueryHandler(SignInManager<Domain.Account.Account> signInManager,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public Task<AuthenticationProperties> Handle(GetExternalLoginProviderPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, request.ReturnUrl);

        return Task.FromResult(properties);
    }
}
