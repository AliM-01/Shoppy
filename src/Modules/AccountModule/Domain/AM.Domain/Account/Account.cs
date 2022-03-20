using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AM.Domain.Account;

[BsonCollection("accounts")]
public class Account : MongoIdentityUser<Guid>
{
    #region Properties

    [Display(Name = "نام")]
    [BsonElement("firstName")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [BsonElement("lastName")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string LastName { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("avatar")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string Avatar { get; set; } = "default-avatar.png";

    [BsonElement("serialNumber")]
    public string SerialNumber { get; set; } = Guid.NewGuid().ToString("N");

    [BsonElement("authenticationTokens")]
    public List<UserToken> AuthenticationTokens { get; set; }

    [BsonElement("userUsedDiscountCodes")]
    public HashSet<UserUsedDiscountCode> UserUsedDiscountCodes { get; set; }

    #endregion Properties

}
