namespace AM.Application.Contracts.Account.DTOs;

public class ForgotPasswordDto
{
    [JsonProperty("email")]
    public string Email { get; set; }
}
