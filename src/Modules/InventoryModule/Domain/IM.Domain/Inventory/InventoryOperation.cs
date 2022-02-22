using _0_Framework.Domain.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IM.Domain.Inventory;

[BsonCollection("inventoryOperations")]
public class InventoryOperation : EntityBase
{
    protected InventoryOperation() { }

    public InventoryOperation(bool operationType, long count, long operatorId, long currentCount,
        string description, long orderId, string invetoryId)
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

    [BsonElement("operationType")]
    public bool OperationType { get; set; }

    [BsonElement("count")]
    public long Count { get; set; }

    [BsonElement("unitPrice")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime OperationDate { get; set; }

    [BsonElement("currentCount")]
    public long CurrentCount { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    #endregion

    #region Relations

    [BsonElement("inventoryId")]
    public string InventoryId { get; set; }

    [BsonElement("inventory")]
    public Inventory Inventory { get; set; }

    [BsonElement("operatorId")]
    public long OperatorId { get; set; }

    [BsonElement("orderId")]
    public long OrderId { get; set; }

    #endregion
}

