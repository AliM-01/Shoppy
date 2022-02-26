using _0_Framework.Application.Exceptions;
using AM.Infrastructure.Persistence.Settings;
namespace AM.Application.Account.CommandHandles;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<string>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticateUserCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager,
                                         SignInManager<Domain.Account.Account> signInManager,
                                         IOptionsSnapshot<JwtSettings> jwtSettings)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
        _jwtSettings = jwtSettings.Value;
    }

    #endregion Ctor

    public async Task<Response<string>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Account.Email);

        if (user is null)
            throw new ApiException($"کاربری با این ایمیل ${request.Account.Email} پیدا نشد.");

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Account.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
            throw new ApiException("کاربری با این مشخصات وارد شده پیدا نشد.");

        if (!user.EmailConfirmed)
            throw new ApiException("حساب کاربری شما فعال نیست");

        return new Response<string>("احراز هویت با موفقیت انجام شد");
    }
}
