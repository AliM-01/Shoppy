using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Slider.DTOs;
public class CreateSliderDto
{
    [Display(Name = "عنوان")]
    [JsonProperty("heading")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(50, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [JsonProperty("text")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(150, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageTitle { get; set; }

    [Display(Name = "لینک")]
    [JsonProperty("btnLink")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [JsonProperty("btnText")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(35, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string BtnText { get; set; }

}