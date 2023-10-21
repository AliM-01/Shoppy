using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SM.Application.ProductPicture.DTOs;
public class CreateProductPictureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFiles")]
    public List<IFormFile> ImageFiles { get; set; }
}