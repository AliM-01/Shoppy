using _0_Framework.Application.Models.Seo;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Product.DTOs.Admin;

public class CreateProductDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "شناسه دسته بندی")]
    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [JsonProperty("shortDescription")]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }
}
