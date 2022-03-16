using _0_Framework.Domain;
using _0_Framework.Domain.Attributes;
using _0_Framework.Domain.Seo;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.Article;

[BsonCollection("articles")]
public class Article : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Title { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    [BsonElement("summary")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Summary { get; set; }

    [Display(Name = "متن")]
    [BsonElement("text")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MinLength(35, ErrorMessage = DomainErrorMessage.MinLength)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    [BsonElement("slug")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
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
