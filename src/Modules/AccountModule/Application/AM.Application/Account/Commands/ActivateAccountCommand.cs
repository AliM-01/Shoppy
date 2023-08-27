using _0_Framework.Application.Exceptions;
using AM.Application.Account.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AM.Application.Account.Commands;

public record ActivateAccountCommand(ActivateAccountRequestDto Account) : IRequest<ApiResult>;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, ApiResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ActivateAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<ApiResult> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Account.UserId)));

        NotFoundApiException.ThrowIfNull(user);

        string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Account.ActiveToken));

        var res = await _userManager.ConfirmEmailAsync(user, token);

        if (!res.Succeeded)
            throw new ApiException(res.Errors.First().Description);

        return ApiResponse.Success("حساب کاربری شما با موفقیت فعال شد");
    }
}
