namespace AM.Application.Contracts.Account.DTOs;

public class AuthenticateUserDto
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}
