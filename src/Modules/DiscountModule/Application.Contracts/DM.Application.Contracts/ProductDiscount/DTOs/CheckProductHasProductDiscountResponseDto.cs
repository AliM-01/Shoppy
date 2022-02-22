namespace DM.Application.Contracts.ProductDiscount.DTOs;

public class CheckProductHasProductDiscountResponseDto
{
    [JsonProperty("existsProductDiscount")]
    public bool ExistsProductDiscount { get; set; }
}
