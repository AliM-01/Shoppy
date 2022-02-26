namespace AM.Application.Contracts.Account.DTOs;

public class ForgotPasswordDto
{
    [JsonProperty("mobile")]
    [Display(Name = "تلفن همراه")]
    public string Mobile { get; set; }
}
