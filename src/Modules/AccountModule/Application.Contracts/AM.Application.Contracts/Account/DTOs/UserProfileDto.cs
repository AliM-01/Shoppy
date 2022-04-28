namespace AM.Application.Contracts.Account.DTOs;
public class UserProfileDto
{
    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("avatarBase64")]
    public string AvatarBase64 { get; set; }
}
