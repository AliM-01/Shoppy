namespace OM.Application.Contracts.Order.DTOs;
public class UserOrderDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

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
    [JsonProperty("state")]
    public bool State { get; set; }

    [Display(Name = "حساب")]
    [JsonProperty("issueTrackingNo")]
    public string IssueTrackingNo { get; set; }

    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}
