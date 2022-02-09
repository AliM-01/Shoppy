using _0_Framework.Domain.Seo;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.Article;

public class Article : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    public string Summary { get; set; }

    [Display(Name = "متن")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    public string Slug { get; set; }

    [Display(Name = "عنوان لینک")]
    public string CanonicalAddress { get; set; }

    #endregion

    #region Relations

    public long CategoryId { get; set; }

    public ArticleCategory.ArticleCategory Categoy { get; set; }

    #endregion
}
