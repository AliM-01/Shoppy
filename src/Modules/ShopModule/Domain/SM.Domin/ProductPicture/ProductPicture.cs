namespace SM.Domain.ProductPicture;

[BsonCollection("productPictures")]
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

    #endregion
}