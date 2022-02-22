namespace SM.Domain.ProductCategory;

[BsonCollection("productCategories")]
public class ProductCategory : MongoSeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [BsonElement("description")]
    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    [Required]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    [BsonElement("slug")]
    [Required]
    public string Slug { get; set; }

    #endregion
}