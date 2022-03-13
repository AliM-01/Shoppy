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
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [BsonElement("lastName")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string LastName { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("avatar")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string Avatar { get; set; }

    [BsonElement("serialNumber")]
    public string SerialNumber { get; set; }

    [BsonElement("authenticationTokens")]
    public List<UserToken> AuthenticationTokens { get; set; }

    [BsonElement("userUsedDiscountCodes")]
    public HashSet<UserUsedDiscountCode> UserUsedDiscountCodes { get; set; }

    #endregion Properties

}
