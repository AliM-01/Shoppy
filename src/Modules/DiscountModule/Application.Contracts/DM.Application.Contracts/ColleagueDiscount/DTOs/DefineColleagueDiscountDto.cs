namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class DefineColleagueDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public long ProductId { get; set; }

    [Display(Name = "درصد")]
    [JsonProperty("rate")]
    public int Rate { get; set; }
}