namespace OM.Application.Order.DTOs;

public class CartItemDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("slug")]
    public string Slug { get; set; }

    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonProperty("unitPriceWithDiscount")]
    public decimal UnitPriceWithDiscount { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [JsonProperty("imageAlt")]
    public string ImageAlt { get; set; }

    [JsonProperty("imageTitle")]
    public string ImageTitle { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("totalItemPrice")]
    public decimal TotalItemPrice { get; set; }

    [JsonProperty("isNotInStock")]
    public bool IsNotInStock { get; set; }

    [JsonProperty("itemInInventoryCountIsLowerThanRequestedCount")]
    public bool ItemInInventoryCountIsLowerThanRequestedCount { get; set; }

    [JsonProperty("discountRate")]
    public int DiscountRate { get; set; }

    [JsonProperty("discountAmount")]
    public decimal DiscountAmount { get; set; }

    [JsonProperty("itemPayAmount")]
    public decimal ItemPayAmount { get; set; }

    public CartItemDto()
    {
        TotalItemPrice = UnitPrice * Count;
        IsNotInStock = false;
        ItemInInventoryCountIsLowerThanRequestedCount = false;
    }

    public void CalculateTotalItemPrice()
    {
        TotalItemPrice = UnitPrice * Count;
    }
}