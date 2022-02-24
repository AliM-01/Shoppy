using MongoDB.Bson;

namespace DM.Domain.ProductDiscount;

[BsonCollection("productDiscounts")]
public class ProductDiscount : EntityBase
{
    #region Properties

    [Display(Name = "درصد")]
    [BsonElement("rate")]
    [Range(0, 100)]
    public int Rate { get; set; }

    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    [Required]
    public DateTime EndDate { get; set; }

    [BsonElement("description")]
    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    #endregion

    #region Relations

    [Display(Name = "محصول")]
    [BsonElement("productId")]
    [Required]
    public string ProductId { get; set; }

    #endregion
}