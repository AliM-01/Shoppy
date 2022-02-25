using Newtonsoft.Json;

namespace AM.Application.Contracts.Account.DTOs;

public class AccountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("fullName")]
    public string FullName { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [JsonProperty("registerDate")]
    public string RegisterDate { get; set; }
}
