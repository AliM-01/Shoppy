using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Extensions;

namespace AM.Application.Account.CommandHandles;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResult>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ForgotPasswordCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<ApiResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Password.Email);

        if (user is null)
            throw new ApiException("کاربری با این مشخصات پیدا نشد");

        var newPassword = Generator.Password();

        await _userManager.RemovePasswordAsync(user);

        await _userManager.AddPasswordAsync(user, newPassword);

        user.SerialNumber = Guid.NewGuid().ToString("N");

        await _userManager.UpdateAsync(user);

        return ApiResponse.Success("رمز عبور شما تغییر کرد و برای شما ارسال شد. لطفا بعد از ورود به حساب آن را تغییر دهید");
    }
}
