using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Utilities.ImageRelated;
using AM.Application.Account.DTOs;
using AM.Application.Account.Queries;

namespace AM.Application.Account.Queries;

public record GetUserProfileQuery(string UserId) : IRequest<UserProfileDto>;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;

    public GetUserProfileQueryHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByIdAsync(request.UserId);

        NotFoundApiException.ThrowIfNull(account);

        return _mapper.Map(account, new UserProfileDto
        {
            AvatarBase64 = ImageHelper.ConvertToBase64($"{PathExtension.Avatar200}/{account.Avatar}")
        });
    }
}
