namespace SM.Domain.Slider;

public class Slider : BaseEntity
{
    #region Properties

    [Display(Name = "عنوان")]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    public string ImagePath { get; set; }

    [Display(Name = "جزییات تصویر")]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    public string ImageTitle { get; set; }

    [Display(Name = "لینک")]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    public string BtnText { get; set; }

    #endregion
}
