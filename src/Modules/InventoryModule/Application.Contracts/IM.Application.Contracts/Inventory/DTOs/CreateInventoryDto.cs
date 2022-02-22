namespace IM.Application.Contracts.Inventory.DTOs;

public class CreateInventoryDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public long ProductId { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }
}
