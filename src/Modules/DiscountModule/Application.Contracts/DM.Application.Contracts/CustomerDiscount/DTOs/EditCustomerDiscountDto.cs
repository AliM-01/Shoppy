namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class EditCustomerDiscountDto : CreateCustomerDiscountDto
{
    [JsonProperty("id")]
    public long Id { get; set; }
}