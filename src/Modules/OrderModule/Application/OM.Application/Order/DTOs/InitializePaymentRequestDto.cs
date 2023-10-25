namespace OM.Application.Order.DTOs;

public class InitializePaymentRequestDto
{
    public InitializePaymentRequestDto(string oId, decimal amount, string callBackUrl, string email)
    {
        OrderId = oId;
        Amount = amount;
        CallBackUrl = callBackUrl;
        Email = email;
    }
    [JsonProperty("orderId")]
    public string OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("callBackUrl")]
    public string CallBackUrl { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }
}
