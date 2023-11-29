namespace IM.Application.Inventory.DTOs;

public class CreateInventoryDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }
}
