namespace SM.Application.Contracts.ProductFeature.DTOs;

public class EditProductFeatureDto : CreateProductFeatureDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}