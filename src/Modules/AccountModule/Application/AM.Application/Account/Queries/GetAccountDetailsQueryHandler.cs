using _0_Framework.Application.Exceptions;
using AM.Application.Account.DTOs;

namespace AM.Application.Account.Queries;

public record GetAccountDetailsQuery(string UserId) : IRequest<EditAccountDto>;

public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, EditAccountDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public GetAccountDetailsQueryHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<EditAccountDto> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByIdAsync(request.UserId);

        NotFoundApiException.ThrowIfNull(account);

        return _mapper.Map<EditAccountDto>(account);
    }
}
