using _0_Framework.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CM.Domain.Comment;

public class Comment : EntityBase
{
    #region Properties

    [Display(Name = "نام")]
    [BsonElement("name")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Display(Name = "ایمیل")]
    [BsonElement("email")]
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Display(Name = "متن نظر")]
    [BsonElement("text")]
    [Required]
    [MaxLength(500)]
    public string Text { get; set; }

    [Display(Name = "وضعیت")]
    [BsonElement("state")]
    [BsonRepresentation(BsonType.String)]
    public CommentState State { get; set; } = CommentState.UnderProgress;

    [Display(Name = "نوع")]
    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public CommentType Type { get; set; } = CommentType.Product;

    [Display(Name = "شناسه محصول/مقاله")]
    [BsonElement("ownerRecordId")]
    public long OwnerRecordId { get; set; }

    #endregion

    #region Relations

    [Display(Name = "شناسه والد")]
    [BsonElement("parentId")]
    public string? ParentId { get; set; }

    #endregion
}

public enum CommentState
{
    [Display(Name = "در حال بررسی")]
    [BsonRepresentation(BsonType.String)]
    UnderProgress,
    [Display(Name = "رد شده")]
    [BsonRepresentation(BsonType.String)]
    Canceled,
    [Display(Name = "تایید شده")]
    [BsonRepresentation(BsonType.String)]
    Confirmed
}

public enum CommentType
{
    [Display(Name = "محصول")]
    [BsonRepresentation(BsonType.String)]
    Product,
    [Display(Name = "مقاله")]
    [BsonRepresentation(BsonType.String)]
    Article
}
