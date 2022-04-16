namespace AM.Application.Contracts.Account.DTOs;
public class RegisterAccountResponseDto
{
    public RegisterAccountResponseDto(string token)
    {
        Token = token;
    }
    [JsonIgnore]
    public string Token { get; set; }

    [JsonProperty("callBackUrl")]
    public string CallBackUrl { get; set; } = "";
}
