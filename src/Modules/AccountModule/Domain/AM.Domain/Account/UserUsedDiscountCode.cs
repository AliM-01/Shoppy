using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace AM.Domain.Account;

[BsonCollection("userUsedDiscountCodes")]
public class UserUsedDiscountCode : EntityBase
{
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("discountCodeId")]
    public string DiscountCodeId { get; set; }
}
