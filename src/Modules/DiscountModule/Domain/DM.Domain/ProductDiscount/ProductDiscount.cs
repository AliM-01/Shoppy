using MongoDB.Bson;

namespace DM.Domain.ProductDiscount;

[BsonCollection("productDiscounts")]
public class ProductDiscount : EntityBase
{
    #region Properties

    [Display(Name = "درصد")]
    [BsonElement("rate")]
    [Range(1, 100, ErrorMessage = DomainErrorMessage.Required)]
    public int Rate { get; set; }

    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public DateTime EndDate { get; set; }

    [BsonElement("description")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLength)]
    public string Description { get; set; }

    [BsonElement("isExpired")]
    [Display(Name = "منقضی شده")]
    public bool IsExpired {
        get {
            return (StartDate > DateTime.Now || EndDate <= DateTime.Now ? true : false);
        }
    }

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    [Required(ErrorMessage = DomainErrorMessage.Required)]
    public string ProductId { get; set; }

    #endregion
}