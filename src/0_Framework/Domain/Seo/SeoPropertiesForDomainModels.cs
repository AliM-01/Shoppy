using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Domain.Seo;

public class SeoPropertiesForDomainModels : EntityBase
{
    [Display(Name = "جزییات تصویر")]
    [BsonElement("imageAlt")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [BsonElement("imageTitle")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100)]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [BsonElement("metaKeywords")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(80)]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [BsonElement("metaDescription")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100)]
    public string MetaDescription { get; set; }
}
