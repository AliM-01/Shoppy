using _0_Framework.Application.Exceptions;
namespace AM.Application.Account.CommandHandles;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<string>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ChangePasswordCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            throw new ApiException("کاربری با این مشخصات پیدا نشد");

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password.OldPassword);

        if (!isCorrectPassword)
            throw new ApiException("رمز فعلی وارد شده اشتباه است");

        var changePasswordResult = await _userManager
            .ChangePasswordAsync(user, request.Password.OldPassword, request.Password.NewPassword);

        if (!changePasswordResult.Succeeded)
            throw new ApiException("رمز عبور جدید نباید با رمز عبور فعلی یکسان باشد");

        user.SerialNumber = Guid.NewGuid().ToString("N");

        await _userManager.UpdateAsync(user);

        return new Response<string>("حساب کاربری شما با موفقیت ویرایش شد");
    }
}
