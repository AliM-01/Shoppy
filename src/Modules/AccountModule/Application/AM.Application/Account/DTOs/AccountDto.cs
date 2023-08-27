namespace AM.Application.Account.DTOs;

public class AccountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("roles")]
    public HashSet<string> Roles { get; set; }

    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatarPath")]
    public string AvatarPath { get; set; }

    [JsonProperty("registerDate")]
    public string RegisterDate { get; set; }
}
