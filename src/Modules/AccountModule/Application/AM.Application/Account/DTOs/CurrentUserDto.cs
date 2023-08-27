namespace AM.Application.Account.DTOs;
public class CurrentUserDto
{
    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatarPath")]
    public string AvatarPath { get; set; }

    [JsonProperty("registerDate")]
    public string RegisterDate { get; set; }
}
