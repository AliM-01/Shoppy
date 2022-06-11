namespace DM.Domain.DiscountCode;

[BsonCollection("userUsedDiscountCodes")]
public class UserUsedDiscountCode : EntityBase
{
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("discountCodeId")]
    public string DiscountCodeId { get; set; }
}
