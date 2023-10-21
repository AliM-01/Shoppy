namespace SM.Application.ProductFeature.DTOs;

public class EditProductFeatureDto : CreateProductFeatureDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}