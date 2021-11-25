using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.ProductPicture.DTOs;
public class CreateProductPictureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "تصویر")]
    [JsonProperty("imageFile")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "جزییات تصویر")]
    [JsonProperty("imageAlt")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [JsonProperty("imageTitle")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string ImageTitle { get; set; }
}