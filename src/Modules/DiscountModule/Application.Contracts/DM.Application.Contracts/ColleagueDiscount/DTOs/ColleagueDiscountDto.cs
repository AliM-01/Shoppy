namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class ColleagueDiscountDto
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

    [Display(Name = "وضعیت فعال بودن")]
    [JsonProperty("isActive")]
    public string IsActive { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}