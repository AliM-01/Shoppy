using _0_Framework.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IM.Domain.Inventory;

[BsonCollection("inventories")]
public class Inventory : EntityBase
{
    public Inventory() { }

    public Inventory(string productId, decimal unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        InStock = false;
    }

    #region Properties

    [Display(Name = "قیمت")]
    [BsonElement("unitPrice")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public decimal UnitPrice { get; set; }

    [Display(Name = "وضعیت")]
    [BsonElement("inStock")]
    public bool InStock { get; set; } = false;

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    public string ProductId { get; set; }

    [BsonElement("operations")]
    public List<InventoryOperation> Operations { get; set; }

    #endregion
}

