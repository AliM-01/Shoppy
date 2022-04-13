using _0_Framework.Application.Exceptions;

namespace AM.Application.Account.CommandHandles;

public class DeActivateAccountCommandHandler : IRequestHandler<DeActivateAccountCommand, ApiResult>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public DeActivateAccountCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<ApiResult> Handle(DeActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.AccountId);

        if (user is null)
            throw new NotFoundApiException();

        user.EmailConfirmed = false;

        await _userManager.UpdateAsync(user);

        return ApiResponse.Success();
    }
}
