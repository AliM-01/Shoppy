using _0_Framework.Application.Models.Seo;

namespace _01_Shoppy.Query.Models.ProductCategory;

public class ProductCategoryQueryModel : SeoPropertiesForApplicationModels
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

    [Display(Name = "عنوان لینک")]
    [JsonProperty("slug")]
    public string Slug { get; set; }
}
