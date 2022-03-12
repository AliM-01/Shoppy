namespace OM.Domain.Order;

[BsonCollection("orders")]
public class Order : EntityBase
{
    #region Properties

    [Display(Name = "حساب")]
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("totalAmount")]
    public decimal TotalAmount { get; set; }

    [BsonElement("discountAmount")]
    public decimal DiscountAmount { get; set; }

    [BsonElement("paymentAmount")]
    public decimal PaymentAmount { get; set; }

    [BsonElement("isPaid")]
    public bool IsPaid { get; set; }

    [BsonElement("isCanceled")]
    public bool IsCanceled { get; set; }

    [Display(Name = "کد پیگیری")]
    [BsonElement("issueTrackingNo")]
    [Required]
    [MaxLength(8)]
    public string IssueTrackingNo { get; set; }

    [Display(Name = "کد بازگشت درگاه پرداخت")]
    [BsonElement("refId")]
    public long RefId { get; set; }

    [BsonElement("items")]
    public List<OrderItem> Items { get; set; }

    #endregion
}
