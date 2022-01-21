namespace SM.Application.Contracts.ProductFeature.DTOs;

public class EditProductFeatureDto : CreateProductFeatureDto
{
    [JsonProperty("id")]
    public long Id { get; set; }
}