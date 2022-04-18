using _0_Framework.Application.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

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
        var user = await _userManager.FindByIdAsync(Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Account.UserId)));

        if (user is null)
            throw new NotFoundApiException();

        string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Account.ActiveToken));

        var res = await _userManager.ConfirmEmailAsync(user, token);

        if (!res.Succeeded)
            throw new ApiException(res.Errors.First().Description);

        return ApiResponse.Success("حساب کاربری شما با موفقیت فعال شد");
    }
}
