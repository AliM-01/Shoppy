namespace SM.Domain.ProductFeature;

public class ProductFeature : BaseEntity
{
    #region Properties

    [Display(Name = "شناسه محصول")]

    public long? ProductId { get; set; }

    [Display(Name = "عنوان ویژگی")]
    public string FeatureTitle { get; set; }

    [Display(Name = "مقدار ویژگی")]
    public string FeatureValue { get; set; }

    #endregion Properties

    #region Relations

    public virtual Product.Product Product { get; set; }

    #endregion Relations
}