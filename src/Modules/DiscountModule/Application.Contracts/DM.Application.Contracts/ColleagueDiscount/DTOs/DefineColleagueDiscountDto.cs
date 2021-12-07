namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class DefineColleagueDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 100000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "درصد")]
    [JsonProperty("rate")]
    [Range(1, 100, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public int Rate { get; set; }
}