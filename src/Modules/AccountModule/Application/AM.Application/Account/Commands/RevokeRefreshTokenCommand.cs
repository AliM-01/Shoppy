using _0_Framework.Application.Exceptions;
using AM.Application.Account.DTOs;
using AM.Application.Services;

namespace AM.Application.Account.Commands;

public record RevokeRefreshTokenCommand(RevokeRefreshTokenRequestDto Token) : IRequest<AuthenticateUserResponseDto>;

public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, AuthenticateUserResponseDto>
{
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

    public async Task<AuthenticateUserResponseDto> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(request.Token.RefreshToken))
            throw new ApiException("لطفا توکن معتبر وارد کنید");

        var token = await _tokenStoreService.FindToken(request.Token.RefreshToken);

        if (token == null)
            throw new ApiException("لطفا توکن معتبر وارد کنید");

        var user = await _userManager.FindByIdAsync(token.UserId);

        NotFoundApiException.ThrowIfNull(user, "کاربری با اطلاعات خواسته شده پیدا نشد");

        var result = await _tokenFactoryService.CreateJwtTokenAsync(user);
        await _tokenStoreService.AddUserToken(user, result.RefreshTokenSerial, result.AccessToken,
                _tokenFactoryService.GetRefreshTokenSerial(request.Token.RefreshToken));

        return new AuthenticateUserResponseDto(result);
    }
}