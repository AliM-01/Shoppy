using MongoDB.Bson;

namespace DM.Domain.DiscountCode;

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

    [BsonElement("isExpired")]
    [Display(Name = "منقضی شده")]
    public bool IsExpired {
        get {
            if (StartDate > DateTime.Now || EndDate <= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
