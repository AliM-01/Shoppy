namespace IM.Domain.Inventory;

public class InventoryOperation
{
    #region Properties

    public long Id { get; set; }

    public bool OperationType { get; set; }

    public long Count { get; set; }

    public DateTime OperationDate { get; set; }

    public long CurrentCount { get; set; }

    public string Description { get; set; }

    #endregion

    #region Relations

    public long InventoryId { get; set; }

    public Inventory Inventory { get; set; }

    public long OperatorId { get; set; }

    public long OrderId { get; set; }

    #endregion
}

