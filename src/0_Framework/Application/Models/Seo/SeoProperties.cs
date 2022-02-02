using _0_Framework.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.Models.Seo;

public class SeoProperties
{
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
