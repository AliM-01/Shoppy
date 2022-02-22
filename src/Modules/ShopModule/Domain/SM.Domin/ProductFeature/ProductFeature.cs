namespace SM.Domain.ProductFeature;

[BsonCollection("productFeatures")]
public class ProductFeature : EntityBase
{
    #region Properties

    [Display(Name = "شناسه محصول")]
    [BsonElement("productId")]
    public string ProductId { get; set; }

    [Display(Name = "عنوان ویژگی")]
    [BsonElement("featureTitle")]
    public string FeatureTitle { get; set; }

    [Display(Name = "مقدار ویژگی")]
    [BsonElement("featureValue")]
    public string FeatureValue { get; set; }

    #endregion Properties

    #region Relations

    [BsonElement("product")]
    public virtual Product.Product Product { get; set; }

    #endregion Relations
}