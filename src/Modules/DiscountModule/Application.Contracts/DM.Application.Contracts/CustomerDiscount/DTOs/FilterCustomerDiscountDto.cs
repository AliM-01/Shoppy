using System.Collections.Generic;

namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class FilterCustomerDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ProductId { get; set; }

    [JsonProperty("discounts")]
    public IEnumerable<CustomerDiscountDto> Discounts { get; set; }
}