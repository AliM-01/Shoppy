namespace SM.Domain.ProductPicture;

[BsonCollection("productPicture")]
public class ProductPicture : EntityBase
{
    #region Properties

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    public string ImagePath { get; set; }

    #endregion

    #region Relations

    [BsonElement("productId")]
    public string ProductId { get; set; }

    [BsonElement("product")]
    public virtual Product.Product Product { get; set; }

    #endregion
}