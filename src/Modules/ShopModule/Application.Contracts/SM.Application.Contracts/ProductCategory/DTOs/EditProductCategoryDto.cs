namespace SM.Application.Contracts.ProductCategory.DTOs;

public class EditProductCategoryDto : CreateProductCategoryDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("imagePath")]
    public string ImagePath { get; set; }
}