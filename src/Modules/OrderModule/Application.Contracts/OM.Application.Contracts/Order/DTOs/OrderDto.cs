namespace OM.Application.Contracts.Order.DTOs;

public class OrderDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "شناسه کاربر")]
    [JsonProperty("accountId")]
    public string AccountId { get; set; }

    [Display(Name = "نام کاربر")]
    [JsonProperty("userFullName")]
    public string UserFullName { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("totalAmount")]
    public double TotalAmount { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("discountAmount")]
    public double DiscountAmount { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("paymentAmount")]
    public double PaymentAmount { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("isPaid")]
    public bool IsPaid { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("isCanceled")]
    public bool IsCanceled { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("issueTrackingNo")]
    public string IssueTrackingNo { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("refId")]
    public long RefId { get; set; }

    [JsonProperty("creationDate")]
    public DateTime CreationDate { get; set; }
}
