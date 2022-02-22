using _0_Framework.Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SM.Domain.Product;

[BsonCollection("products")]
public class Product : MongoSeoPropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("title")]
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Display(Name = "کد")]
    [BsonElement("code")]
    [Required]
    [MaxLength(15)]
    public string Code { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [BsonElement("shortDescription")]
    [Required]
    [MaxLength(250)]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [BsonElement("description")]
    [Required]
    [MaxLength(2500)]
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

    #region Relations

    [BsonElement("categoryId")]
    public string CategoryId { get; set; }

    [BsonElement("category")]
    public virtual ProductCategory.ProductCategory Category { get; set; }

    [BsonElement("productPictures")]
    public virtual ICollection<ProductPicture.ProductPicture> ProductPictures { get; set; }

    [BsonElement("productFeatures")]
    public virtual ICollection<ProductFeature.ProductFeature> ProductFeatures { get; set; }


    #endregion
}
