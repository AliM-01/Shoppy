using System.Collections.Generic;

namespace SM.Domain.ProductCategory;

public class ProductCategory : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    public string Slug { get; set; }

    #endregion

    #region Relations
    public virtual ICollection<Product.Product> Products { get; set; }

    #endregion
}