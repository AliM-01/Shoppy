namespace OM.Application.Order.DTOs;

public class PlaceOrderResponseDto
{
    public PlaceOrderResponseDto(string orderId)
    {
        OrderId = orderId;
    }
    [Display(Name = "شناسه سفارش")]
    [JsonProperty("orderId")]
    public string OrderId { get; set; }
}
