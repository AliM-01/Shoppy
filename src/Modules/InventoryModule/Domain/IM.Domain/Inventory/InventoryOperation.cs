using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IM.Domain.Inventory;

public class InventoryOperation : BaseEntity
{
    protected InventoryOperation() { }

    public InventoryOperation(bool operationType, long count, long operatorId, long currentCount,
        string description, long orderId, Guid invetoryId)
    {
        OperationType = operationType;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = invetoryId;
        OperationDate = DateTime.Now;
    }

    #region Properties

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new Guid Id { get; set; } = Guid.NewGuid();

    public bool OperationType { get; set; }

    public long Count { get; set; }

    public DateTime OperationDate { get; set; }

    public long CurrentCount { get; set; }

    public string Description { get; set; }

    #endregion

    #region Relations

    public Guid InventoryId { get; set; }

    public Inventory Inventory { get; set; }

    public long OperatorId { get; set; }

    public long OrderId { get; set; }

    #endregion
}

