using System.Collections.Generic;

namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class FilterCustomerDiscountDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [JsonProperty("discounts")]
    public IEnumerable<CustomerDiscountDto> Discounts { get; set; }
}