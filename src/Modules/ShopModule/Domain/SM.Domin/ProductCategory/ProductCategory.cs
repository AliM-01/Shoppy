namespace SM.Domain.ProductCategory;

[BsonCollection("productCategories")]
public class ProductCategory : SeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [BsonElement("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
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
}