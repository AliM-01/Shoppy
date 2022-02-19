using Microsoft.AspNetCore.Http;

namespace BM.Application.Contracts.Article.DTOs;

public class CreateArticleDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "شناسه دسته بندی")]
    [JsonProperty("categoryId")]
    public string CategoryId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    public string Title { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    [JsonProperty("summary")]
    public string Summary { get; set; }

    [Display(Name = "متن")]
    [JsonProperty("text")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("canonicalAddress")]
    public string CanonicalAddress { get; set; }
}
