namespace AM.Application.Contracts.Account.DTOs;

public class AuthenticateUserDto
{
    [JsonProperty("mobile")]
    [Display(Name = "موبایل")]
    public string Mobile { get; set; }

    [JsonProperty("password")]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; }
}
