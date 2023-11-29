namespace DM.Application.ProductDiscount.DTOs;
public class DefineProductDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

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
}