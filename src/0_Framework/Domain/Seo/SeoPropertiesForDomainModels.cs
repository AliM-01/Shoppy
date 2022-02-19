using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Domain.Seo;

public class SeoPropertiesForDomainModels : BaseEntity
{
    [Display(Name = "جزییات تصویر")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    public string MetaDescription { get; set; }
}

public class MongoSeoPropertiesForDomainModels : EntityBase
{
    [Display(Name = "جزییات تصویر")]
    [BsonElement("imageAlt")]
    [Required]
    [MaxLength(100)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [BsonElement("imageTitle")]
    [Required]
    [MaxLength(100)]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [BsonElement("metaKeywords")]
    [Required]
    [MaxLength(80)]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [BsonElement("metaDescription")]
    [Required]
    [MaxLength(100)]
    public string MetaDescription { get; set; }
}
