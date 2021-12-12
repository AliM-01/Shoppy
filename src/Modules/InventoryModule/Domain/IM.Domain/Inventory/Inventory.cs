namespace IM.Domain.Inventory;

public class Inventory : BaseEntity
{
    #region Properties

    public double UnitPrice { get; set; }

    public bool InStock { get; set; } = false;

    #endregion

    #region Relations

    public long ProductId { get; set; }

    public ICollection<InventoryOperation> Operations { get; set; }

    #endregion

    #region Methods

    public long CalculateCurrentCount()
    {
        var plus = Operations.Where(x => x.OperationType).Sum(x => x.Count);
        var minus = Operations.Where(x => !x.OperationType).Sum(x => x.Count);
        return plus - minus;
    }

    public void Increase(long count, long operatorId, string description)
    {
        var currentCount = CalculateCurrentCount() + count;
        var operation = new InventoryOperation
        {
            OperationType = true,
            Count = count,
            OperatorId = operatorId,
            Description = description,
            CurrentCount = currentCount,
            OrderId = 0,
            InventoryId = Id
        };
        Operations.Add(operation);
        InStock = currentCount > 0;
    }

    public void Reduce(long count, long operatorId, string description, long orderId)
    {
        var currentCount = CalculateCurrentCount() - count;
        var operation = new InventoryOperation
        {
            OperationType = false,
            Count = count,
            OperatorId = operatorId,
            Description = description,
            CurrentCount = currentCount,
            OrderId = orderId,
            InventoryId = Id
        };
        Operations.Add(operation);
        InStock = currentCount > 0;
    }

    #endregion


}

