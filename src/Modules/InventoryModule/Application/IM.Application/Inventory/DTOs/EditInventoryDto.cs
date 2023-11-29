namespace IM.Application.Inventory.DTOs;

public class EditInventoryDto : CreateInventoryDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}
