using _01_Shoppy.Query.Models.ProductPicture;
using SM.Application.ProductFeature.DTOs;

namespace _01_Shoppy.Query.Models.Product;

public class ProductDetailsQueryModel : ProductQueryModel
{
    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [JsonProperty("shortDescription")]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "موجودی")]
    [JsonProperty("inventoryCurrentCount")]
    public long InventoryCurrentCount { get; set; }

    [Display(Name = "تصاویر محصول")]
    [JsonProperty("productPictures")]
    public List<ProductPictureQueryModel> ProductPictures { get; set; }

    [Display(Name = "ویژگی های محصول")]
    [JsonProperty("productFeatures")]
    public List<ProductFeatureDto> ProductFeatures { get; set; }
}
