namespace SM.Application.Contracts.ProductFeature.DTOs;
public class CreateProductFeatureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    [Range(1, 10000, ErrorMessage = DomainErrorMessage.RequiredMessage)]
    public long ProductId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("featureTitle")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(100, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string FeatureTitle { get; set; }

    [Display(Name = "مقدار")]
    [JsonProperty("featureValue")]
    [Required(ErrorMessage = DomainErrorMessage.RequiredMessage)]
    [MaxLength(250, ErrorMessage = DomainErrorMessage.MaxLengthMessage)]
    public string FeatureValue { get; set; }
}