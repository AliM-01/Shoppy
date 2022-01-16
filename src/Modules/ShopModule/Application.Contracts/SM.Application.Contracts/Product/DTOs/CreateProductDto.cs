using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Product.DTOs;

public class CreateProductDto
{
    [Display(Name = "شناسه دسته بندی")]
    [JsonProperty("categoryId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long CategoryId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("title")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیح کوتاه")]
    [JsonProperty("shortDescription")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string Description { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    [MaxFileSize((3 * 1024 * 1024), ErrorMessage = DomainErrorMessage.FileMaxSizeMessage)]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(200, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(200, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [JsonProperty("metaKeywords")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(80, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [JsonProperty("metaDescription")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string MetaDescription { get; set; }

}
