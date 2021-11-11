using _0_Framework.Domain;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductCategory.DTOs;
public class FilterProductCategoryDto
{
    [JsonProperty("productTitle")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string Title { get; set; }

    [JsonProperty("productCategories")]
    public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
}