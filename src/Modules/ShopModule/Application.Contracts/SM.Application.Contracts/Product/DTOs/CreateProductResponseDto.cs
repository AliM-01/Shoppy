namespace SM.Application.Contracts.Product.DTOs;


public class CreateProductResponseDto
{
    public CreateProductResponseDto(long productId)
    {
        ProductId = productId;
    }

    [JsonProperty("productId")]
    public long ProductId { get; set; }
}

