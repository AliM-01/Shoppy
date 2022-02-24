using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CM.Domain.Comment;

[BsonCollection("comments")]
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
    [BsonRepresentation(BsonType.Int32)]
    public CommentState State { get; set; } = CommentState.UnderProgress;

    [Display(Name = "نوع")]
    [BsonElement("type")]
    [BsonRepresentation(BsonType.Int32)]
    public CommentType Type { get; set; } = CommentType.Product;

    [Display(Name = "شناسه محصول/مقاله")]
    [BsonElement("ownerRecordId")]
    public string OwnerRecordId { get; set; }

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
    [BsonRepresentation(BsonType.Int32)]
    UnderProgress,
    [Display(Name = "رد شده")]
    [BsonRepresentation(BsonType.Int32)]
    Canceled,
    [Display(Name = "تایید شده")]
    [BsonRepresentation(BsonType.Int32)]
    Confirmed
}

public enum CommentType
{
    [Display(Name = "محصول")]
    [BsonRepresentation(BsonType.Int32)]
    Product,
    [Display(Name = "مقاله")]
    [BsonRepresentation(BsonType.Int32)]
    Article
}
