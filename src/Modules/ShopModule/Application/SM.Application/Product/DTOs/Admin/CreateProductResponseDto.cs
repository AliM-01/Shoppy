namespace SM.Application.Product.DTOs.Admin;

public class CreateProductResponseDto
{
    public CreateProductResponseDto(string productId)
    {
        ProductId = productId;
    }

    [JsonProperty("productId")]
    public string ProductId { get; set; }
}

