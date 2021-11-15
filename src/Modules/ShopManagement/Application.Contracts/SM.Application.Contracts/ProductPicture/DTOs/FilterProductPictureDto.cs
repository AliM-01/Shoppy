using System.Collections.Generic;

namespace SM.Application.Contracts.ProductPicture.DTOs;
public class FilterProductPictureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [JsonProperty("productPictures")]
    public IEnumerable<ProductPictureDto> ProductPictures { get; set; }
}