using Microsoft.AspNetCore.Http;

namespace BM.Application.Contracts.ArticleCategory.DTOs;

public class CreateArticleCategoryDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [JsonProperty("orderShow")]
    public int OrderShow { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("canonicalAddress")]
    public string CanonicalAddress { get; set; }
}
