namespace OM.Application.Contracts.Order.DTOs;

public class CartDto
{
    public CartDto()
    {
        Items = new List<CartItemDto>();
    }

    [Display(Name = "جمع کل")]
    [JsonProperty("totalAmount")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "مقدار تخفیف")]
    [JsonProperty("discountAmount")]
    public decimal DiscountAmount { get; set; }

    [Display(Name = "مقدار قابل پرداخت")]
    [JsonProperty("payAmount")]
    public decimal PayAmount { get; set; }

    [Display(Name = "آیتم ها")]
    [JsonProperty("items")]
    public List<CartItemDto> Items { get; set; }
}
