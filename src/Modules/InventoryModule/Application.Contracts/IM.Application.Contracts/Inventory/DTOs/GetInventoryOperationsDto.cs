namespace IM.Application.Contracts.Inventory.DTOs;

public class GetInventoryOperationsDto
{
    public GetInventoryOperationsDto(string inventoryId, string productId, InventoryOperationDto[] operations)
    {
        InventoryId = inventoryId;
        ProductId = productId;
        Operations = operations;
    }

    [Display(Name = "شناسه انبار")]
    [JsonProperty("inventoryId")]
    public string InventoryId { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "محصول")]
    [JsonProperty("productTitle")]
    public string ProductTitle { get; set; }

    [JsonProperty("operations")]
    public InventoryOperationDto[] Operations { get; set; }
}
