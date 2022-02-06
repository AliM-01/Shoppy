using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Product.DTOs;

public class CreateProductDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "شناسه دسته بندی")]
    [JsonProperty("categoryId")]
    public long CategoryId { get; set; }

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
