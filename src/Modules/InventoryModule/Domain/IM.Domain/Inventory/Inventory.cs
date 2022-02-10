using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new Guid Id { get; set; } = Guid.NewGuid();

    public double UnitPrice { get; set; }

    public bool InStock { get; set; } = false;

    #endregion

    #region Relations

    public long ProductId { get; set; }

    public virtual ICollection<InventoryOperation> Operations { get; set; }

    #endregion
}

