namespace IM.Domain.Inventory;

public class Inventory : BaseEntity
{
    protected Inventory() { }

    public Inventory(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStock = false;
    }

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
        Operations.Add(new InventoryOperation(true, count, operatorId, currentCount, description, 0, this.Id));
        InStock = currentCount > 0;
    }

    public void Reduce(long count, long operatorId, string description, long orderId)
    {
        var currentCount = CalculateCurrentCount() - count;
        Operations.Add(new InventoryOperation(false, count, operatorId, currentCount, description, orderId, this.Id));
        InStock = currentCount > 0;
    }

    #endregion


}

