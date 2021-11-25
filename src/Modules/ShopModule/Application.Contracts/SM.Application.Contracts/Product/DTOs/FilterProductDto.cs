using System.Collections.Generic;

namespace SM.Application.Contracts.Product.DTOs;
public class FilterProductDto
{
    [JsonProperty("search")]
    [Display(Name = "جستجو")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Search { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "دسته بندی")]
    public long CategoryId { get; set; }

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; }
}