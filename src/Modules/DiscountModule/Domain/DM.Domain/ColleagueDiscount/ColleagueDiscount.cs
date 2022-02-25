namespace DM.Domain.ColleagueDiscount;

[BsonCollection("colleagueDiscounts")]
public class ColleagueDiscount : EntityBase
{
    #region Properties

    [Display(Name = "درصد")]
    [BsonElement("rate")]
    [Range(1, 100, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public int Rate { get; set; }

    [Display(Name = "وضعیت")]
    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string ProductId { get; set; }

    #endregion
}