namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class DefineCustomerDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "درصد")]
    [JsonProperty("rate")]
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
    public string Description { get; set; }
}