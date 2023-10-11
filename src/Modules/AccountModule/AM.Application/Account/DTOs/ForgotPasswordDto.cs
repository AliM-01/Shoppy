namespace AM.Application.Account.DTOs;

public class ForgotPasswordDto
{
    [JsonProperty("email")]
    public string Email { get; set; }
}
