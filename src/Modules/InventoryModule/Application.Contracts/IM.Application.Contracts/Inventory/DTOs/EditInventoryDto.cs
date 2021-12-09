namespace IM.Application.Contracts.Inventory.DTOs;

public class EditInventoryDto : CreateInventoryDto
{
    [JsonProperty("id")]
    public long Id { get; set; }
}
