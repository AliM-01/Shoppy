using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _0_Framework.Domain;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; }

    [BsonElement("creationDate")]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [BsonElement("lastUpdateDate")]
    public DateTime LastUpdateDate { get; set; } = DateTime.Now;
}
