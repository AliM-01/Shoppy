namespace SM.Domain.Slider;

[BsonCollection("slider")]
public class Slider : ImagePropertiesForDomainModels
{
    #region Properties

    [Display(Name = "عنوان")]
    [BsonElement("heading")]
    [Required]
    [MaxLength(100)]
    public string Heading { get; set; }

    [Display(Name = "متن")]
    [BsonElement("text")]
    [Required]
    [MaxLength(250)]
    public string Text { get; set; }

    [Display(Name = "تصویر")]
    [BsonElement("imagePath")]
    public string ImagePath { get; set; }

    [Display(Name = "لینک")]
    [BsonElement("btnLink")]
    [Required]
    public string BtnLink { get; set; }

    [Display(Name = "متن لینک")]
    [BsonElement("btnText")]
    [Required]
    [MaxLength(50)]
    public string BtnText { get; set; }

    #endregion
}
