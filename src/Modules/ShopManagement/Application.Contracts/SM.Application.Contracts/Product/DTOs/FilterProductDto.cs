using System.Collections.Generic;

namespace SM.Application.Contracts.Product.DTOs;
public class FilterProductDto
{
    [JsonProperty("search")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Search { get; set; }

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; }
}