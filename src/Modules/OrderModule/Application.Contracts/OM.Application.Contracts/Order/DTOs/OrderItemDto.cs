namespace OM.Application.Contracts.Order.DTOs;

public class OrderItemDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "محصول")]
    [JsonProperty("product")]
    public string Product { get; set; }

    [Display(Name = "تعداد")]
    [JsonProperty("count")]
    public int Count { get; set; }

    [Display(Name = "قیمت محصول")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "درصد تخفیف")]
    [JsonProperty("discountRate")]
    public int DiscountRate { get; set; }

    [Display(Name = "شناسه سفارش")]
    [JsonProperty("orderId")]
    public string OrderId { get; set; }
}
