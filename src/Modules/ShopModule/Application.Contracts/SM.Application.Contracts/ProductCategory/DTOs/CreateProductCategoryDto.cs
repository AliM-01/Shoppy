using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.ProductCategory.DTOs;
public class CreateProductCategoryDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }
}