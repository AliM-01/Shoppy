namespace SM.Application.Contracts.Product.DTOs;

public class ExistsProductIdResponseDto
{
    [JsonProperty("exists")]
    public bool Exists { get; set; }

    [JsonProperty("productId")]
    public long ProductId { get; set; }

    [JsonProperty("productTitle")]
    public string ProductTitle { get; set; }
}
