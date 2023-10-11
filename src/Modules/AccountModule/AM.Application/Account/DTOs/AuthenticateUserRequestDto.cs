namespace AM.Application.Account.DTOs;

public class AuthenticateUserRequestDto
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}