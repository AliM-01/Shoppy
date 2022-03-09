namespace OM.Application.Contracts.Order.DTOs;

public class CartItemInCookieDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "عنوان محصول")]
    [JsonProperty("productTitle")]
    public string ProductTitle { get; set; }

    [JsonProperty("productSlug")]
    public string ProductSlug { get; set; }

    [JsonProperty("productImg")]
    public string ProductImg { get; set; }

    [Display(Name = "قیمت محصول")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }
}
