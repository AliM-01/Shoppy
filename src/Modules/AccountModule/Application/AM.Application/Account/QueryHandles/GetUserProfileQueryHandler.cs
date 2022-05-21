using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Utilities.ImageRelated;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;

namespace AM.Application.Account.QueryHandles;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public GetUserProfileQueryHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion Ctor

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByIdAsync(request.UserId);

        if (account is null)
            throw new NotFoundApiException();

        return _mapper.Map(account, new UserProfileDto
        {
            AvatarBase64 = ImageHelper.ConvertToBase64($"{PathExtension.Avatar200}/{account.Avatar}")
        });
    }
}
