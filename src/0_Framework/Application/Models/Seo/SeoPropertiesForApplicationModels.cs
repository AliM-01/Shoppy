using _0_Framework.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.Models.Seo;

public class SeoPropertiesForApplicationModels
{
    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(200, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(200, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string ImageTitle { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [JsonProperty("metaKeywords")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(80, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string MetaKeywords { get; set; }

    [Display(Name = "توضیحات Meta")]
    [JsonProperty("metaDescription")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string MetaDescription { get; set; }
}
