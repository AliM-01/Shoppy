namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class CustomerDiscountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "محصول")]
    [JsonProperty("product")]
    public string Product { get; set; }

    [Display(Name = "درصد")]
    [JsonProperty("rate")]
    public int Rate { get; set; }

    [Display(Name = "تاریخ شروع")]
    [JsonProperty("startDate")]
    public string StartDate { get; set; }

    [Display(Name = "تاریخ پایان")]
    [JsonProperty("endDate")]
    public string EndDate { get; set; }

    [Display(Name = "توضیحات")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "وضعیت فعال بودن")]
    [JsonProperty("isExpired")]
    public string IsExpired { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}