using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Utilities.ImageRelated;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;

namespace AM.Application.Account.QueryHandles;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ApiResult<UserProfileDto>>
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

    public async Task<ApiResult<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByIdAsync(request.UserId);

        if (account is null)
            throw new NotFoundApiException();

        var mappedAccount = _mapper.Map<UserProfileDto>(account);

        mappedAccount.AvatarBase64 = ImageHelper.ConvertToBase64($"{PathExtension.Avatar200}/{account.Avatar}");

        return ApiResponse.Success<UserProfileDto>(mappedAccount);
    }
}
