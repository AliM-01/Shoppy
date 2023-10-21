namespace SM.Application.ProductFeature.DTOs;

public class ProductFeatureDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display(Name = "شناسه محصول")]
    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [Display(Name = "عنوان")]
    [JsonProperty("featureTitle")]
    public string FeatureTitle { get; set; }

    [Display(Name = "مقدار")]
    [JsonProperty("featureValue")]
    public string FeatureValue { get; set; }

    [Display(Name = "تاریخ ثبت")]
    [JsonProperty("creationDate")]
    public string CreationDate { get; set; }
}