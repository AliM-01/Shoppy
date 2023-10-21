namespace SM.Application.ProductFeature.DTOs;
public class CreateProductFeatureDto
{
    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("featureTitle")]
    public string FeatureTitle { get; set; }

    [Display(Name = "مقدار")]
    [JsonProperty("featureValue")]
    public string FeatureValue { get; set; }
}