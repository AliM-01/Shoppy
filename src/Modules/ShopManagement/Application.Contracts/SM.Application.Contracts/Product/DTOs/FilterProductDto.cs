using System.Collections.Generic;

namespace SM.Application.Contracts.Product.DTOs;
public class FilterProductDto
{
    [JsonProperty("productTitle")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [JsonProperty("code")]
    [Display(Name = "کد")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Code { get; set; }

    [JsonProperty("categoryId")]
    [Display(Name = "شناسه دسته بندی")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string CategoryId { get; set; }

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; }
}