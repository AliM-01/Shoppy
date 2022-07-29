using _0_Framework.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.Models.Seo;

public class ImagePropertiesForApplicationModels
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
}