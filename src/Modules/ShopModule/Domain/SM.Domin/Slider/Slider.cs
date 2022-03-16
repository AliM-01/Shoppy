namespace SM.Domain.Slider;

[BsonCollection("sliders")]
public class Slider : ImagePropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("heading")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [BsonElement("text")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "لینک")]
    [BsonElement("btnLink")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [BsonElement("btnText")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(50, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string BtnText { get; set; }

    #endregion
}
