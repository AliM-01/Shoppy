namespace AM.Application.Contracts.Account.DTOs;

public class RevokeRefreshTokenRequestDto
{
    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}
