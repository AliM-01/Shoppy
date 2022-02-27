using AM.Application.Contracts.Common;

namespace AM.Application.Contracts.Services;

public interface ITokenFactoryService
{
    Task<JwtTokenResponse> CreateJwtTokensAsync(Domain.Account.Account user);

    string GetRefreshTokenSerial(string refreshTokenValue);
}
