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

    public virtual ICollection<InventoryOperation> Operations { get; set; }

    #endregion
}

