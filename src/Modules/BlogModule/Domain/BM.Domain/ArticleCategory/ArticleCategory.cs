using _0_Framework.Domain;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.ArticleCategory;

public class ArticleCategory : BaseEntity
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "ترتیب نمایش")]
    public int OrderShow { get; set; }

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    [Display(Name = "جزییات تصویر")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    public string MetaDescription { get; set; }

    [Display(Name = "عنوان لینک")]
    public string Slug { get; set; }

    [Display(Name = "عنوان لینک")]
    public string CanonicalAddress { get; set; }

    #endregion 
}
