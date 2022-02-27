using AM.Domain.Account;

namespace AM.Application.Contracts.Services;

public interface ITokenStoreService
{
    Task AddUserToken(UserToken userToken);

    Task AddUserToken(Domain.Account.Account user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial);

    Task<bool> IsValidToken(string accessToken, int userId);

    Task DeleteExpiredTokens();

    Task<UserToken> FindToken(string refreshTokenValue);

    Task DeleteToken(string refreshTokenValue);

    Task DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource);

    Task InvalidateUserTokens(int userId);

    Task RevokeUserBearerTokens(string userIdValue, string refreshTokenValue);
}
