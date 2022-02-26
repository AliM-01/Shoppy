namespace AM.Application.Contracts.Account.DTOs;

public class ChangePasswordDto
{
    [JsonProperty("oldPassword")]
    public string OldPassword { get; set; }

    [JsonProperty("newPassword")]
    public string NewPassword { get; set; }

    [JsonProperty("confirmPassword")]
    public string ConfirmNewPassword { get; set; }
}
