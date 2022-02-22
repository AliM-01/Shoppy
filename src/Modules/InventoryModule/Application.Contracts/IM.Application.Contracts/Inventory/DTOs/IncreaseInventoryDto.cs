namespace IM.Application.Contracts.Inventory.DTOs;

public class IncreaseInventoryDto
{
    [Display(Name = "شناسه انبار")]
    [JsonProperty("inventoryId")]
    public string InventoryId { get; set; }

    [Display(Name = "تعداد")]
    [JsonProperty("count")]
    public long Count { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }
}
