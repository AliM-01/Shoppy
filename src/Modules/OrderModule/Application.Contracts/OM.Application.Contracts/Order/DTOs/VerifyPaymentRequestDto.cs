namespace OM.Application.Contracts.Order.DTOs;

public class VerifyPaymentRequestDto
{
    [JsonProperty("authority")]
    public string Authority { get; set; }

    [JsonProperty("orderId")]
    public string OrderId { get; set; }
}
