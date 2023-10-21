using _0_Framework.Application.Models.Seo;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Slider.DTOs;
public class CreateSliderDto : ImagePropertiesForApplicationModels
{
    [Display(Name = "عنوان")]
    [JsonProperty("heading")]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [JsonProperty("text")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "لینک")]
    [JsonProperty("btnLink")]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [JsonProperty("btnText")]
    public string BtnText { get; set; }

}