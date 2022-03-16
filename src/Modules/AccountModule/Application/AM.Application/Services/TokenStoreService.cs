using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.IRepository;
using AM.Application.Contracts.Common.Settings;
using AM.Application.Contracts.Services;
using AM.Domain.Account;
using MongoDB.Driver;

namespace AM.Application.Services;

public class TokenStoreService : ITokenStoreService
{
    #region ctor

    private readonly ISecurityService _securityService;
    private readonly IRepository<UserToken> _userTokenRepository;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly BearerTokenSettings _tokenSettings;
    private readonly ITokenFactoryService _tokenFactoryService;

    public TokenStoreService(
        UserManager<Domain.Account.Account> userManager,
        IRepository<UserToken> userTokenRepository,
        ISecurityService securityService,
        IOptionsSnapshot<BearerTokenSettings> tokenSettings,
        ITokenFactoryService tokenFactoryService)
    {
        _securityService = Guard.Against.Null(securityService, nameof(_securityService));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _userTokenRepository = Guard.Against.Null(userTokenRepository, nameof(_userTokenRepository));
        _tokenSettings = tokenSettings.Value;
        _tokenFactoryService = Guard.Against.Null(tokenFactoryService, nameof(_tokenFactoryService));
    }

    #endregion

    #region AddUserToken

    public async Task AddUserToken(UserToken userToken)
    {
        if (!_tokenSettings.AllowMultipleLoginsFromTheSameUser)
        {
            await InvalidateUserTokens(userToken.UserId);
        }
        await DeleteTokensWithSameRefreshTokenSource(userToken.RefreshTokenIdHashSource);
        await _userTokenRepository.InsertAsync(userToken);
    }

    public async Task AddUserToken(Domain.Account.Account user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial)
    {
        var now = DateTimeOffset.UtcNow;
        var token = new UserToken
        {
            UserId = user.Id.ToString(),
            RefreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial),
            RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                                       null : _securityService.GetSha256Hash(refreshTokenSourceSerial),

            AccessTokenHash = _securityService.GetSha256Hash(accessToken),
            RefreshTokenExpiresDateTime = now.AddMinutes(_tokenSettings.RefreshTokenExpirationHours),
            AccessTokenExpiresDateTime = now.AddMinutes(_tokenSettings.AccessTokenExpirationMinutes)
        };
        await AddUserToken(token);
    }

    #endregion

    #region Delete Expired Tokens

    public async Task DeleteExpiredTokens()
    {
        var now = DateTimeOffset.UtcNow;

        var expiredTokens = await _userTokenRepository
            .AsQueryable()
            .Where(x => x.RefreshTokenExpiresDateTime < now)
            .ToListAsyncSafe();

        for (int i = 0; i < expiredTokens.Count; i++)
        {
            await _userTokenRepository.DeleteAsync(expiredTokens[i].Id);
        }
    }

    #endregion

    #region Delete Token

    public async Task DeleteToken(string refreshTokenValue)
    {
        var token = await FindToken(refreshTokenValue);

        if (token != null)
            await _userTokenRepository.DeleteAsync(token.Id);
    }

    #endregion

    #region Delete Tokens With Same Refresh Token Source

    public async Task DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource)
    {
        if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
            return;

        var tokens = await _userTokenRepository
            .AsQueryable()
            .Where(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource ||
                                 t.RefreshTokenIdHash == refreshTokenIdHashSource &&
                                  t.RefreshTokenIdHashSource == null)
            .ToListAsyncSafe();

        for (int i = 0; i < tokens.Count; i++)
        {
            await _userTokenRepository.DeleteAsync(tokens[i].Id);
        }
    }

    #endregion

    #region Revoke User Bearer Tokens

    public async Task RevokeUserBearerTokens(string userIdValue, string refreshTokenValue)
    {
        if (!string.IsNullOrWhiteSpace(userIdValue))
        {
            if (_tokenSettings.AllowSignoutAllUserActiveClients)
            {
                await InvalidateUserTokens(userIdValue);
            }
        }

        if (!string.IsNullOrWhiteSpace(refreshTokenValue))
        {
            var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
            if (!string.IsNullOrWhiteSpace(refreshTokenSerial))
            {
                var refreshTokenIdHashSource = _securityService.GetSha256Hash(refreshTokenSerial);
                await DeleteTokensWithSameRefreshTokenSource(refreshTokenIdHashSource);
            }
        }

        await DeleteExpiredTokens();
    }

    #endregion

    #region Find Token

    public async Task<UserToken> FindToken(string refreshTokenValue)
    {
        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            return null;

        var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);

        if (string.IsNullOrWhiteSpace(refreshTokenSerial))
            return null;

        var refreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial);

        var filter = Builders<UserToken>.Filter.Eq(x => x.RefreshTokenIdHash, refreshTokenIdHash);

        return await _userTokenRepository.GetByFilter(filter);
    }

    #endregion

    #region Invalidate User Tokens

    public async Task InvalidateUserTokens(string userId)
    {
        var tokens = await _userTokenRepository
            .AsQueryable()
            .Where(x => x.UserId == userId)
            .ToListAsyncSafe();

        for (int i = 0; i < tokens.Count; i++)
        {
            await _userTokenRepository.DeleteAsync(tokens[i].Id);
        }
    }

    #endregion

    #region Is Valid Token

    public async Task<bool> IsValidToken(string accessToken, string userId)
    {
        var accessTokenHash = _securityService.GetSha256Hash(accessToken);

        var userToken = await _userTokenRepository
            .AsQueryable()
            .Where(x => x.AccessTokenHash == accessTokenHash && x.UserId == userId)
            .ToListAsyncSafe();

        return userToken.First().AccessTokenExpiresDateTime >= DateTimeOffset.UtcNow;
    }

    #endregion
}
