namespace OM.Application.Contracts.Order.DTOs;

public class InitializePaymentResponseDto
{
    [JsonProperty("redirectUrl")]
    public string RedirectUrl { get; set; }
}
