using _0_Framework.Domain.Attributes;
using _0_Framework.Domain.Seo;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.Article;

[BsonCollection("ArticleCollection")]
public class Article : MongoSeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    [BsonElement("summary")]
    [Required]
    [MaxLength(100)]
    public string Summary { get; set; }

    [Display(Name = "متن")]
    [BsonElement("text")]
    [Required]
    [MinLength(35)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    [Required]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    [BsonElement("slug")]
    [Required]
    public string Slug { get; set; }

    [Display(Name = "عنوان لینک")]
    [BsonElement("canonicalAddress")]
    public string CanonicalAddress { get; set; }

    #endregion

    #region Relations

    [BsonElement("categoryId")]
    public string CategoryId { get; set; }

    [BsonElement("category")]
    public ArticleCategory.ArticleCategory Category { get; set; }

    #endregion
}
