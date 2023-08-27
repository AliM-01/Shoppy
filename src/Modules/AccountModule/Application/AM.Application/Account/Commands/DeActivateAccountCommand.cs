using _0_Framework.Application.Exceptions;

namespace AM.Application.Account.Commands;

public record DeActivateAccountCommand(string AccountId) : IRequest<ApiResult>;

public class DeActivateAccountCommandHandler : IRequestHandler<DeActivateAccountCommand, ApiResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public DeActivateAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<ApiResult> Handle(DeActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId);

        NotFoundApiException.ThrowIfNull(user);

        user.EmailConfirmed = false;

        await _userManager.UpdateAsync(user);

        return ApiResponse.Success();
    }
}
