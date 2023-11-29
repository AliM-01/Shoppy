using _0_Framework.Application.Models.Seo;

namespace SM.Application.Product.DTOs.Site;

public class ProductSiteDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "شناسه")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "قیمت")]
    [JsonProperty("price")]
    public string Price { get; set; }

    [Display(Name = "قیمت با تخفیف")]
    [JsonProperty("priceWithDiscount")]
    public string PriceWithDiscount { get; set; }

    [Display(Name = "درصد تخفیف")]
    [JsonProperty("discountRate")]
    public int DiscountRate { get; set; }

    [Display(Name = "دسته بندی")]
    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [Display(Name = "دسته بندی")]
    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("categorySlug")]
    public string CategorySlug { get; set; }

    [Display(Name = "تخفیف دارد ؟")]
    [JsonProperty("hasDiscount")]
    public bool HasDiscount { get; set; } = false;

    [Display(Name = "عنوان لینک")]
    [JsonProperty("slug")]
    public string Slug { get; set; }
}
