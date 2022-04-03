namespace OM.Application.Contracts.Order.DTOs;

public class VerifyPaymentRequestDto
{
    public VerifyPaymentRequestDto(string authority, string oId)
    {
        Authority = authority;
        OrderId = oId;
    }
    [JsonProperty("authority")]
    public string Authority { get; set; }

    [JsonProperty("orderId")]
    public string OrderId { get; set; }
}
