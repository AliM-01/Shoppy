using _0_Framework.Application.Extensions;

namespace OM.Application.Contracts.Order.DTOs;

public class CartItemDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

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

    [JsonProperty("isInStock")]
    public bool IsInStock { get; set; }

    [JsonProperty("discountRate")]
    public int DiscountRate { get; set; }

    [JsonProperty("discountAmount")]
    public decimal DiscountAmount { get; set; }

    [JsonProperty("itemPayAmount")]
    public decimal ItemPayAmount { get; set; }

    public CartItemDto()
    {
        Id = Generators.GenerateCode(subString: 8);
        TotalItemPrice = UnitPrice * Count;
    }

    public void CalculateTotalItemPrice()
    {
        TotalItemPrice = UnitPrice * Count;
    }
}