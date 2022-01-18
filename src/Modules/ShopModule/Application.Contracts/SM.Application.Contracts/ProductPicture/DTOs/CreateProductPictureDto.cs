﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductPicture.DTOs;
public class CreateProductPictureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFiles")]
    public List<IFormFile> ImageFiles { get; set; }
}