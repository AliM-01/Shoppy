namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class DefineCustomerDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 100000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "درصد")]
    [JsonProperty("rate")]
    [Range(1, 100, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public int Rate { get; set; }

    [Display(Name = "تاریخ شروع")]
    [JsonProperty("startDate")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string StartDate { get; set; }

    [Display(Name = "تاریخ پایان")]
    [JsonProperty("endDate")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public string EndDate { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Description { get; set; }
}