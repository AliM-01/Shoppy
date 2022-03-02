using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AM.Domain.Account;

[BsonCollection("userTokens")]
public class UserToken : EntityBase
{
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("accessTokenHash")]
    public string AccessTokenHash { get; set; }

    [BsonElement("accessTokenExpiresDateTime")]
    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

    [BsonElement("refreshTokenIdHash")]
    public string RefreshTokenIdHash { get; set; }

    [BsonElement("refreshTokenIdHashSource")]
    public string RefreshTokenIdHashSource { get; set; }

    [BsonElement("refreshTokenExpiresDateTime")]
    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }
}
