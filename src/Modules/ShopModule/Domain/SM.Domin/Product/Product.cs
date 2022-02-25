using System.Collections.Generic;

namespace SM.Domain.Product;

[BsonCollection("products")]
public class Product : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "کد")]
    [BsonElement("code")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(15, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Code { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [BsonElement("shortDescription")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [BsonElement("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    [BsonElement("slug")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string Slug { get; set; }

    #endregion

    #region Relations

    [BsonElement("categoryId")]
    public string CategoryId { get; set; }

    [BsonElement("category")]
    public ProductCategory.ProductCategory Category { get; set; }

    [BsonElement("productPictures")]
    public List<ProductPicture.ProductPicture> ProductPictures { get; set; }

    [BsonElement("productFeatures")]
    public List<ProductFeature.ProductFeature> ProductFeatures { get; set; }


    #endregion
}
