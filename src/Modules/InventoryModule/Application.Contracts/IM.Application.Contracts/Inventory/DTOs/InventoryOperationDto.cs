namespace IM.Application.Contracts.Inventory.DTOs;

public class InventoryOperationDto
{
    public long Id { get; set; }
    public bool OperationType { get; set; }

    public long Count { get; set; }

    public string OperationDate { get; set; }

    public long CurrentCount { get; set; }

    public string Description { get; set; }

    public long InventoryId { get; set; }

    public long OperatorId { get; set; }

    public string Operator { get; set; }

    public long OrderId { get; set; }
}
