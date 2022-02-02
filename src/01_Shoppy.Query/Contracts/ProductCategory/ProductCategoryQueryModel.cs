using _0_Framework.Application.Models.Seo;

namespace _01_Shoppy.Query.Contracts.ProductCategory;

public class ProductCategoryQueryModel : SeoProperties
{
    [Display(Name = "شناسه")]
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("slug")]
    public string Slug { get; set; }

    [Display(Name = "محصولات")]
    [JsonProperty("products")]
    public List<ProductQueryModel> Products { get; set; } = new List<ProductQueryModel>();
}
