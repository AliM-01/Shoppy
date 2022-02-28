namespace AM.Application.Contracts.Account.DTOs;

public class AuthenticateUserResponseDto
{
    [JsonProperty("accessToken")]
    public string AccessToken { get; set; }

    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}
