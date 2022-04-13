using _0_Framework.Application.Exceptions;

namespace AM.Application.Account.CommandHandles;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, ApiResult>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ActivateAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<ApiResult> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Account.Email);

        if (user is null)
            throw new ApiException($"کاربری با این ایمیل ${request.Account.Email} پیدا نشد.");

        user.EmailConfirmed = true;

        await _userManager.UpdateAsync(user);

        return ApiResponse.Success("حساب کاربری شما با موفقیت فعال شد");
    }
}
