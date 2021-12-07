using System.Collections.Generic;

namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class FilterColleagueDiscountDto
{
    [JsonProperty("productTitle")]
    [Display(Name = "عنوان محصول")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ProductTitle { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(0, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [JsonProperty("discounts")]
    public IEnumerable<ColleagueDiscountDto> Discounts { get; set; }
}