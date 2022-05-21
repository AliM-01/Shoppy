using _0_Framework.Application.Exceptions;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;

namespace AM.Application.Account.QueryHandles;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, AccountDto>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public GetCurrentUserQueryHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<AccountDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var account = await _userManager.FindByIdAsync(request.UserId);

        NotFoundApiException.ThrowIfNull(account);

        return _mapper.Map(account, new AccountDto
        {
            Roles = (await _userManager.GetRolesAsync(account)).ToHashSet<string>()
        });
    }
}
