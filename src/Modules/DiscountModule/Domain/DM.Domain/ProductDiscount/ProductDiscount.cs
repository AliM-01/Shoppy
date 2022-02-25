using MongoDB.Bson;

namespace DM.Domain.ProductDiscount;

[BsonCollection("productDiscounts")]
public class ProductDiscount : EntityBase
{
    #region Properties

    [Display(Name = "درصد")]
    [BsonElement("rate")]
    [Range(1, 100, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public int Rate { get; set; }

    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public DateTime EndDate { get; set; }

    [BsonElement("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Description { get; set; }

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string ProductId { get; set; }

    #endregion
}