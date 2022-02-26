using _0_Framework.Application.Exceptions;
using AM.Domain.Enums;
using AM.Infrastructure.Persistence.Settings;
namespace AM.Application.Account.CommandHandles;

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, Response<string>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public RegisterAccountCommandHandler(IMapper mapper,
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

    public async Task<Response<string>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Account.Email);

        if (userWithSameEmail != null)
            throw new ApiException($"ایمیل وارد شده <${request.Account.Email}> تکراری می‌ باشد");

        var user = new Domain.Account.Account
        {
            UserName = request.Account.Email,
            Email = request.Account.Email.Trim(),
            Avatar = "default-avatar.png"
        };

        _mapper.Map(request.Account, user);

        var result = await _userManager.CreateAsync(user, request.Account.Password);

        if (!result.Succeeded)
            throw new ApiException($"${result.Errors}");

        await _userManager.AddToRoleAsync(user, Roles.BasicUser.ToString());

        return new Response<string>(user.Id.ToString());
    }
}
