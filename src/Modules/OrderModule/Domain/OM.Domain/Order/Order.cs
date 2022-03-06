using _0_Framework.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OM.Domain.Order;

public class Order : EntityBase
{
    #region Properties

    [Display(Name = "حساب")]
    [BsonElement("accountId")]
    public string AccountId { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("paymentMethod")]
    public int PaymentMethod { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("totalAmount")]
    public double TotalAmount { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("discountAmount")]
    public double DiscountAmount { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("paymentAmount")]
    public double PaymentAmount { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("isPaid")]
    public bool IsPaid { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("isCanceled")]
    public bool IsCanceled { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("issueTrackingNo")]
    public string IssueTrackingNo { get; set; }

    [Display(Name = "حساب")]
    [BsonElement("refId")]
    public long RefId { get; set; }

    #endregion
}
