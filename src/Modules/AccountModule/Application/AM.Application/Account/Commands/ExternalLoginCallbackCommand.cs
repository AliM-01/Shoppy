using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using AM.Application.Account.DTOs;
using AM.Application.Account.Enums;
using AM.Application.Services;
using AM.Domain.Enums;
using System.Security.Claims;

namespace AM.Application.Account.Commands;

public record ExternalLoginCallbackCommand() : IRequest<ExternalLoginCallbackResponseDto>;

public class ExternalLoginCallbackCommandHandler : IRequestHandler<ExternalLoginCallbackCommand, ExternalLoginCallbackResponseDto>
{
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly ITokenFactoryService _tokenFactoryService;
    private readonly ITokenStoreService _tokenStoreService;

    public ExternalLoginCallbackCommandHandler(SignInManager<Domain.Account.Account> signInManager,
                                         UserManager<Domain.Account.Account> userManager,
                                         ITokenFactoryService tokenFactoryService,
                                         ITokenStoreService tokenStoreService)
    {
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _tokenFactoryService = Guard.Against.Null(tokenFactoryService, nameof(_tokenFactoryService));
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
    }

    public async Task<ExternalLoginCallbackResponseDto> Handle(ExternalLoginCallbackCommand request, CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info is null)
            throw new ApiException(ApplicationErrorMessage.UnknownError);

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                                                   info.ProviderKey,
                                                                   false,
                                                                   true);

        if (!result.Succeeded)
            throw new ApiException(ApplicationErrorMessage.UnknownError);

        string? email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (email is null)
            throw new ApiException(ApplicationErrorMessage.UnknownError);

        var user = await _userManager.FindByEmailAsync(email);

        if (user is not null)
            return new ExternalLoginCallbackResponseDto(ExternalLoginCallbackResult.LoginRedirect);

        #region register

        user = new Domain.Account.Account
        {
            Email = email,
            EmailConfirmed = true,
            FirstName = email.Split("@")[0],
            LastName = email.Split("@")[0],
        };

        var registerResult = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            throw new ApiException(registerResult.Errors.First().Description);

        await _userManager.AddToRoleAsync(user, Roles.BasicUser.ToString());
        await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

        await _userManager.AddLoginAsync(user, info);

        await _signInManager.SignInAsync(user, false);

        var token = await _tokenFactoryService.CreateJwtTokenAsync(user);
        await _tokenStoreService.AddUserToken(user, token.RefreshTokenSerial, token.AccessToken, null);

        #endregion register

        return new ExternalLoginCallbackResponseDto
        {
            Type = ExternalLoginCallbackResult.Registered,
            TokenResult = new AuthenticateUserResponseDto(token),
            Email = email
        };
    }
}