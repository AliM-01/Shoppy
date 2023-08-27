using System.Security.Claims;

namespace AM.Application.Models;

public class JwtTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshTokenSerial { get; set; }
    public List<Claim> Claims { get; set; }
}
