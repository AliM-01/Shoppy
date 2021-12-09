namespace IM.Application.Contracts.Inventory.DTOs;

public class CreateInventoryDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 100000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public double UnitPrice { get; set; }
}
