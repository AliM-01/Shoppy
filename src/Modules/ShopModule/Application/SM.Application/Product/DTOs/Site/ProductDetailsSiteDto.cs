using SM.Application.ProductFeature.DTOs;
using SM.Application.ProductPicture.DTOs;

namespace SM.Application.Product.DTOs.Site;

public class ProductDetailsSiteDto : ProductSiteDto
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
    public List<ProductPictureSiteDto> ProductPictures { get; set; }

    [Display(Name = "ویژگی های محصول")]
    [JsonProperty("productFeatures")]
    public List<ProductFeatureDto> ProductFeatures { get; set; }
}
