namespace AM.Application.Account.DTOs;

public class ChangePasswordByAdminDto
{
    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("newPassword")]
    public string NewPassword { get; set; }

    [JsonProperty("confirmPassword")]
    public string ConfirmNewPassword { get; set; }
}
