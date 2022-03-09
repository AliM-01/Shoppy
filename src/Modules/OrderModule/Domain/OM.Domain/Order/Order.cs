namespace OM.Domain.Order;

[BsonCollection("orders")]
public class Order : EntityBase
{
    #region Properties

    [Display(Name = "حساب")]
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("paymentMethod")]
    public int PaymentMethod { get; set; }

    [BsonElement("totalAmount")]
    public double TotalAmount { get; set; }

    [BsonElement("discountAmount")]
    public double DiscountAmount { get; set; }

    [BsonElement("paymentAmount")]
    public double PaymentAmount { get; set; }

    [BsonElement("isPaid")]
    public bool IsPaid { get; set; }

    [BsonElement("isCanceled")]
    public bool IsCanceled { get; set; }

    [Display(Name = "کد پیگیری")]
    [BsonElement("issueTrackingNo")]
    public string IssueTrackingNo { get; set; }

    [Display(Name = "کد بازگشت درگاه پرداخت")]
    [BsonElement("refId")]
    public long RefId { get; set; }

    [BsonElement("items")]
    public List<OrderItem> Items { get; set; }

    #endregion
}
