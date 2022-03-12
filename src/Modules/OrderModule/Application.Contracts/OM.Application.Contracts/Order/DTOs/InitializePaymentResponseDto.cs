namespace OM.Application.Contracts.Order.DTOs;

public class InitializePaymentResponseDto
{
    public InitializePaymentResponseDto(string redirectUrl)
    {
        RedirectUrl = redirectUrl;
    }
    [JsonProperty("redirectUrl")]
    public string RedirectUrl { get; set; }
}
