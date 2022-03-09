using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace OM.Domain.Order;

[BsonCollection("orderItems")]
public class OrderItem : EntityBase
{
    [BsonElement("productId")]
    public string ProductId { get; set; }

    [BsonElement("count")]
    public int Count { get; set; }

    [BsonElement("unitPrice")]
    public decimal UnitPrice { get; set; }

    [BsonElement("discountRate")]
    public int DiscountRate { get; set; }

    [BsonElement("orderId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string OrderId { get; set; }

    [BsonElement("items")]
    public Order Order { get; set; }
}
