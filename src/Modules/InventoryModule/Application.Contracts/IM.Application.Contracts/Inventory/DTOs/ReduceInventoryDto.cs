namespace IM.Application.Contracts.Inventory.DTOs;

public class ReduceInventoryDto
{
    [Display(Name = "شناسه انبار")]
    [JsonProperty("inventoryId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long InventoryId { get; set; }

    [Display(Name = "شناسه سبد خرید")]
    [JsonProperty("orderId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long OrderId { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "تعداد")]
    [JsonProperty("count")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long Count { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Description { get; set; }
}
