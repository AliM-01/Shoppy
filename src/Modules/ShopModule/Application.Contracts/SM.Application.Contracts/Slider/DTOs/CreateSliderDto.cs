using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Slider.DTOs;
public class CreateSliderDto : ImageProperties
{
    [Display(Name = "عنوان")]
    [JsonProperty("heading")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [JsonProperty("text")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    [MaxFileSize((5 * 1024 * 1024), ErrorMessage = DomainErrorMessage.FileMaxSizeMessage)]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "لینک")]
    [JsonProperty("btnLink")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [JsonProperty("btnText")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(50, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string BtnText { get; set; }

}