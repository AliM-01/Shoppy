namespace SM.Domain.Slider;

[BsonCollection("slider")]
public class Slider : ImagePropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("heading")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [BsonElement("text")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "لینک")]
    [BsonElement("btnLink")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [BsonElement("btnText")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(50, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string BtnText { get; set; }

    #endregion
}
