namespace AM.Application.Account.DTOs;

public class RevokeRefreshTokenRequestDto
{
    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}
