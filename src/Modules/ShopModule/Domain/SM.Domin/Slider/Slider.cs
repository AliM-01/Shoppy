namespace SM.Domain.Slider;

public class Slider : ImagePropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    [Display(Name = "لینک")]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    public string BtnText { get; set; }

    #endregion
}
