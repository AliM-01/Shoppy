namespace IM.Application.Contracts.Inventory.DTOs;

public class ReduceInventoryDto
{
    [Display(Name = "شناسه انبار")]
    [JsonProperty("inventoryId")]
    public string InventoryId { get; set; }

    [Display(Name = "شناسه سبد خرید")]
    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public long ProductId { get; set; }

    [Display(Name = "تعداد")]
    [JsonProperty("count")]
    public long Count { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }
}
