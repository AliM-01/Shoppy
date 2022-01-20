using System.Collections.Generic;

namespace SM.Domain.Product;
public class Product : BaseEntity
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "کد")]
    public string Code { get; set; }

    [Display(Name = "توضیح کوتاه")]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

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

    #endregion

    #region Relations

    public long? CategoryId { get; set; }

    public virtual ProductCategory.ProductCategory Category { get; set; }

    public virtual ICollection<ProductPicture.ProductPicture> ProductPictures { get; set; }

    public virtual ICollection<ProductFeature.ProductFeature> ProductFeatures { get; set; }


    #endregion
}
