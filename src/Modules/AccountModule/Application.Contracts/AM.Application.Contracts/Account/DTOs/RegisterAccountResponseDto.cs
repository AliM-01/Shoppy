namespace AM.Application.Contracts.Account.DTOs;
public class RegisterAccountResponseDto
{
    public RegisterAccountResponseDto(string token, string userId, string userEmail, string userFullName)
    {
        Token = token;
        UserId = userId;
        UserEmail = userEmail;
        UserFullName = userFullName;
    }

    [JsonIgnore]
    public string Token { get; set; }

    [JsonProperty("callBackUrl")]
    public string CallBackUrl { get; set; } = "";

    [JsonIgnore]
    public string UserId { get; set; }

    [JsonIgnore]
    public string UserEmail { get; set; }

    [JsonIgnore]
    public string UserFullName { get; set; }
}
