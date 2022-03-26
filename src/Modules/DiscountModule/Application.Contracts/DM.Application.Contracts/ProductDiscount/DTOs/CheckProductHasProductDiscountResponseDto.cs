namespace DM.Application.Contracts.ProductDiscount.DTOs;

public class CheckProductHasProductDiscountResponseDto
{
    public CheckProductHasProductDiscountResponseDto(bool exists)
    {
        ExistsProductDiscount = exists;
    }
    [JsonProperty("existsProductDiscount")]
    public bool ExistsProductDiscount { get; set; }
}
