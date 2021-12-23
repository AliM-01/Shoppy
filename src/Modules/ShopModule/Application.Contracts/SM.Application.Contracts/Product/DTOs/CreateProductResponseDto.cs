namespace SM.Application.Contracts.Product.DTOs;


public class CreateProductResponseDto
{
    [JsonProperty("productId")]
    public long ProductId { get; set; }
}

