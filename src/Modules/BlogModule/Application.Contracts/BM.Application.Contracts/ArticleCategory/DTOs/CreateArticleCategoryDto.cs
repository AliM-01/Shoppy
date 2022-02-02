using Microsoft.AspNetCore.Http;

namespace BM.Application.Contracts.ArticleCategory.DTOs;

public class CreateArticleCategoryDto : SeoPropertiesForApplicationModels
{
    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Description { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public int OrderShow { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    [MaxFileSize((3 * 1024 * 1024), ErrorMessage = DomainErrorMessage.FileMaxSizeMessage)]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "عنوان لینک")]
    [JsonProperty("canonicalAddress")]
    public string CanonicalAddress { get; set; }
}
