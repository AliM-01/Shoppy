namespace SM.Application.Contracts.Product.DTOs;

public class ExistsProductIdResponseDto
{
    [JsonProperty("exists")]
    public bool Exists { get; set; }
}
