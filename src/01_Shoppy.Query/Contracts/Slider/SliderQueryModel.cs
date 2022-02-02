using _0_Framework.Application.Models.Seo;

namespace _01_Shoppy.Query.Contracts.Slider;

public class SliderQueryModel : ImageProperties
{
    [Display(Name = "شناسه")]
    [JsonProperty("id")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("heading")]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [JsonProperty("text")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "لینک")]
    [JsonProperty("btnLink")]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [JsonProperty("btnText")]
    public string BtnText { get; set; }
}
