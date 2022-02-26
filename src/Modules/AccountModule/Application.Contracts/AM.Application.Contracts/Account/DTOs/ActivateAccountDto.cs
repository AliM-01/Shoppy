namespace AM.Application.Contracts.Account.DTOs;

public class ActivateAccountDto
{
    [JsonProperty("mobile")]
    [Display(Name = "تلفن همراه")]
    public string Mobile { get; set; }

    [JsonProperty("activeCode")]
    [Display(Name = "کدفعالسازی")]
    public string ActiveCode { get; set; }
}
