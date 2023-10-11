using _0_Framework.Application.Exceptions;

namespace AM.Application.Account.Commands;

public record ActivateAccountByAdminCommand(string UserId) : IRequest<ApiResult>;

public class ActivateAccountByAdminCommandHandler : IRequestHandler<ActivateAccountByAdminCommand, ApiResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ActivateAccountByAdminCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<ApiResult> Handle(ActivateAccountByAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        NotFoundApiException.ThrowIfNull(user);

        user.EmailConfirmed = true;

        await _userManager.UpdateAsync(user);

        return ApiResponse.Success();
    }
}