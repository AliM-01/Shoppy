namespace DM.Domain.ColleagueDiscount;

[BsonCollection("colleagueDiscounts")]
public class ColleagueDiscount : EntityBase
{
    #region Properties

    [Display(Name = "درصد")]
    [BsonElement("rate")]
    [Range(0, 100)]
    public int Rate { get; set; }

    [Display(Name = "وضعیت")]
    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    [Required]
    public string ProductId { get; set; }

    #endregion
}