namespace DM.Application.Contracts.CustomerDiscount.DTOs;

public class CheckProductHasCustomerDiscountResponseDto
{
    [JsonProperty("existsCustomerDiscount")]
    public bool ExistsCustomerDiscount { get; set; }
}
