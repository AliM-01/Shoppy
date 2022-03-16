using MongoDB.Bson;

namespace DM.Domain.DiscountCode;

[BsonCollection("discountCodes")]
public class DiscountCode : EntityBase
{
    [BsonElement("code")]
    [Display(Name = "کد تخفیف")]
    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    public string Code { get; set; }

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
}
