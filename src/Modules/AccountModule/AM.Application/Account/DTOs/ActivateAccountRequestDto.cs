namespace AM.Application.Account.DTOs;

public class ActivateAccountRequestDto
{
    public ActivateAccountRequestDto(string userId, string activeToken)
    {
        UserId = userId;
        ActiveToken = activeToken;
    }

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("activeToken")]
    public string ActiveToken { get; set; }
}