using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _0_Framework.Domain;

public interface IEntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    string Id { get; set; }

    [BsonElement("isDeleted")]
    bool IsDeleted { get; set; }

    [BsonElement("creationDate")]
    [BsonRepresentation(BsonType.DateTime)]
    DateTime CreationDate { get; set; }

    [BsonElement("lastUpdateDate")]
    [BsonRepresentation(BsonType.DateTime)]
    DateTime LastUpdateDate { get; set; }
}

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; }

    [BsonElement("creationDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [BsonElement("lastUpdateDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime LastUpdateDate { get; set; } = DateTime.Now;
}
