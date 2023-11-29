namespace SM.Application.Product.DTOs.Admin;

public class ExistsProductIdResponseDto
{
    [JsonProperty("exists")]
    public bool Exists { get; set; }

    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("productTitle")]
    public string ProductTitle { get; set; }
}
