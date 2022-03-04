namespace DM.Application.Contracts.DiscountCode.DTOs;
public class DiscountCodeDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "کد")]
    [JsonProperty("code")]
    public string Code { get; set; }

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
}