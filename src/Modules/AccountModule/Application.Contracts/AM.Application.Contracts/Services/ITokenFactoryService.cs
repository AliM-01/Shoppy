using AM.Application.Contracts.Common;

namespace AM.Application.Contracts.Services;

public interface ITokenFactoryService
{
    Task<JwtTokenResponse> CreateJwtTokenAsync(Domain.Account.Account user);

    string GetRefreshTokenSerial(string refreshTokenValue);
}
