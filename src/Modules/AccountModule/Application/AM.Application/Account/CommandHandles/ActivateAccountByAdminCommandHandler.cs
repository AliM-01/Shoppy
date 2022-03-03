using _0_Framework.Application.Exceptions;

namespace AM.Application.Account.CommandHandles;

public class ActivateAccountByAdminCommandHandler : IRequestHandler<ActivateAccountByAdminCommand, Response<string>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public ActivateAccountByAdminCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<Response<string>> Handle(ActivateAccountByAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            throw new NotFoundApiException();

        user.EmailConfirmed = true;

        await _userManager.UpdateAsync(user);

        return new Response<string>();
    }
}
