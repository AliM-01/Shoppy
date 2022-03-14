using _0_Framework.Application.Exceptions;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Services;

namespace AM.Application.Account.CommandHandles;

public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Response<AuthenticateUserResponseDto>>
{
    #region Ctor

    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly SignInManager<Domain.Account.Account> _signInManager;
    private readonly ITokenFactoryService _tokenFactoryService;
    private readonly ITokenStoreService _tokenStoreService;

    public RevokeRefreshTokenCommandHandler(IMapper mapper,
                                         UserManager<Domain.Account.Account> userManager,
                                         SignInManager<Domain.Account.Account> signInManager,
        ITokenFactoryService tokenFactoryService,
ITokenStoreService tokenStoreService)
    {
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _signInManager = Guard.Against.Null(signInManager, nameof(_signInManager));
        _tokenFactoryService = Guard.Against.Null(tokenFactoryService, nameof(_tokenFactoryService));
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
    }

    #endregion Ctor

    public async Task<Response<AuthenticateUserResponseDto>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(request.Token.RefreshToken))
            throw new ApiException("لطفا توکن معتبر وارد کنید");

        var token = await _tokenStoreService.FindToken(request.Token.RefreshToken);

        if (token == null)
            throw new ApiException("لطفا توکن معتبر وارد کنید");

        var user = await _userManager.FindByIdAsync(token.UserId);

        if (user is null)
            throw new NotFoundApiException("کاربری با اطلاعات خواسته شده پیدا نشد");

        var result = await _tokenFactoryService.CreateJwtTokenAsync(user);
        await _tokenStoreService.AddUserToken(user, result.RefreshTokenSerial, result.AccessToken,
                _tokenFactoryService.GetRefreshTokenSerial(request.Token.RefreshToken));

        return new Response<AuthenticateUserResponseDto>(new AuthenticateUserResponseDto(result));
    }
}

