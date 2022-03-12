namespace OM.Application.Contracts.Order.DTOs;

public class InitializePaymentRequestDto
{
    [JsonProperty("orderId")]
    public string OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("callBackUrl")]
    public string CallBackUrl { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }
}
