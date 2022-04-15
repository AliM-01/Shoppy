namespace AM.Application.Contracts.Account.DTOs;
public class RegisterAccountResponseDto
{
    [JsonIgnore]
    public string Token { get; set; }

    [JsonProperty("callBackUrl")]
    public string CallBackUrl { get; set; } = "";
}
