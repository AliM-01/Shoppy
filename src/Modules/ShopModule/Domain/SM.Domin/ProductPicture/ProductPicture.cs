namespace SM.Domain.ProductPicture;

public class ProductPicture : BaseEntity
{
    #region Properties

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    #endregion

    #region Relations

    public long? ProductId { get; set; }

    public virtual Product.Product Product { get; set; }

    #endregion
}