namespace OM.Application.Contracts.Order.DTOs;

public class CartDto
{
    [Display(Name = "جمع کل")]
    [JsonProperty("totalAmount")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "مقدار تخفیف")]
    [JsonProperty("discountAmount")]
    public decimal DiscountAmount { get; set; }

    [Display(Name = "مقدار قابل پرداخت")]
    [JsonProperty("payAmount")]
    public decimal PayAmount { get; set; }

    [Display(Name = "روش پرداخت")]
    [JsonProperty("paymentMethod")]
    public int PaymentMethod { get; set; }

    [Display(Name = "آیتم ها")]
    [JsonProperty("items")]
    public List<CartItemDto> Items { get; set; }
}
