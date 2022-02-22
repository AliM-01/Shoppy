namespace IM.Application.Contracts.Inventory.DTOs;

public class InventoryOperationDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("operationType")]
    public bool OperationType { get; set; }

    [JsonProperty("count")]
    public long Count { get; set; }

    [JsonProperty("operationDate")]
    public string OperationDate { get; set; }

    [JsonProperty("currentCount")]
    public long CurrentCount { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("inventoryId")]
    public string InventoryId { get; set; }

    [JsonProperty("operatorId")]
    public long OperatorId { get; set; }

    [JsonProperty("operator")]
    public string Operator { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }
}
