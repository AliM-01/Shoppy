using _0_Framework.Application.Exceptions;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Services;
namespace AM.Application.Account.CommandHandles;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticateUserResponseDto>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly ITokenFactoryService _tokenFactoryService;
    private readonly ITokenStoreService _tokenStoreService;

    public AuthenticateUserCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager,
                                         SignInManager<Domain.Account.Account> signInManager,
        ITokenFactoryService tokenFactoryService,
ITokenStoreService tokenStoreService)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
        _tokenFactoryService = Guard.Against.Null(tokenFactoryService, nameof(_tokenFactoryService));
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
    }

    #endregion Ctor

    public async Task<Response<AuthenticateUserResponseDto>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Account.Email);

        if (user is null)
            throw new ApiException($"کاربری با این ایمیل ${request.Account.Email} پیدا نشد.");

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Account.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
            throw new ApiException("کاربری با این مشخصات وارد شده پیدا نشد.");

        if (!user.EmailConfirmed)
            throw new ApiException("حساب کاربری شما فعال نیست");

        var token = await _tokenFactoryService.CreateJwtTokenAsync(user);
        await _tokenStoreService.AddUserToken(user, token.RefreshTokenSerial, token.AccessToken, null);

        return new Response<AuthenticateUserResponseDto>(new AuthenticateUserResponseDto(token));
    }
}
