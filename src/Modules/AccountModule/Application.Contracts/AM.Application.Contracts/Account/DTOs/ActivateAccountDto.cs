namespace AM.Application.Contracts.Account.DTOs;

public class ActivateAccountDto
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("activeCode")]
    [Display(Name = "کدفعالسازی")]
    public string ActiveCode { get; set; }
}
