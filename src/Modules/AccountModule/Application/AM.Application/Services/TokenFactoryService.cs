using AM.Application.Contracts.Common;
using AM.Application.Contracts.Services;
using AM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AM.Application.Services;

public class TokenFactoryService : ITokenFactoryService
{
    #region ctor

    private readonly ISecurityService _securityService;
    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly BearerTokenSettings _tokentSettings;
    private readonly ILogger<TokenFactoryService> _logger;

    public TokenFactoryService(
        UserManager<Domain.Account.Account> userManager,
        ISecurityService securityService,
        IOptions<BearerTokenSettings> tokentSettings,
        ILogger<TokenFactoryService> logger)
    {
        _securityService = Guard.Against.Null(securityService, nameof(_securityService));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _tokentSettings = tokentSettings.Value;
        _logger = Guard.Against.Null(logger, nameof(_logger));
    }

    #endregion

    #region Create JwtToken

    public async Task<JwtTokenResponse> CreateJwtTokenAsync(Domain.Account.Account user)
    {
        var (accessToken, claims) = await CreateAccessTokenAsync(user);

        var (refreshTokenValue, refreshTokenSerial) = CreateRefreshToken();

        return new JwtTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            RefreshTokenSerial = refreshTokenSerial,
            Claims = claims
        };
    }

    #endregion

    #region Create Refresh Token

    private (string RefreshTokenValue, string RefreshTokenSerial) CreateRefreshToken()
    {
        string refreshTokenSerial = _securityService.CreateCryptographicallySecureGuid().ToString().Replace("-", "");

        var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _tokentSettings.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _tokentSettings.Issuer, ClaimValueTypes.String, _tokentSettings.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _tokentSettings.Issuer),
                // for invalidation
                new Claim(ClaimTypes.SerialNumber, refreshTokenSerial, ClaimValueTypes.String, _tokentSettings.Issuer)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokentSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
            issuer: _tokentSettings.Issuer,
            audience: _tokentSettings.Audiance,
            claims: claims,
            notBefore: now,
            expires: now.AddHours(_tokentSettings.RefreshTokenExpirationHours),
            signingCredentials: creds);

        string refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return (refreshTokenValue, refreshTokenSerial);
    }

    #endregion

    #region Get Refresh Token Serial

    public string GetRefreshTokenSerial(string refreshTokenValue)
    {
        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            return "";

        var decodedRefreshTokenPrincipal = new ClaimsPrincipal();
        try
        {
            decodedRefreshTokenPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                refreshTokenValue,
                new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokentSettings.Secret)),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                },
                out _
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to validate refreshTokenValue: `{refreshTokenValue}`.");
        }

        return decodedRefreshTokenPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
    }

    #endregion

    #region Create AccessToken Async

    private async Task<(string AccessToken, List<Claim> Claims)> CreateAccessTokenAsync(Domain.Account.Account user)
    {
        var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _tokentSettings.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _tokentSettings.Issuer, ClaimValueTypes.String, _tokentSettings.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _tokentSettings.Issuer),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, _tokentSettings.Issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, _tokentSettings.Issuer),
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String, _tokentSettings.Issuer),
                new Claim("DisplayName", $"{user.FirstName} {user.LastName}", ClaimValueTypes.String, _tokentSettings.Issuer),
                new Claim(ClaimTypes.SerialNumber, user.SerialNumber, ClaimValueTypes.String, _tokentSettings.Issuer),
                new Claim(ClaimTypes.UserData, user.Id.ToString(), ClaimValueTypes.String, _tokentSettings.Issuer)
            };

        // add roles
        var roles = await _userManager.GetRolesAsync(user);
        foreach (string? role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, _tokentSettings.Issuer));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokentSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: _tokentSettings.Issuer,
            audience: _tokentSettings.Audiance,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(_tokentSettings.AccessTokenExpirationMinutes),
            signingCredentials: creds);
        return (new JwtSecurityTokenHandler().WriteToken(token), claims);
    }

    #endregion
}
