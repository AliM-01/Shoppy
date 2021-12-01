namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class EditCustomerDiscountDto : DefineCustomerDiscountDto
{
    [JsonProperty("id")]
    public long Id { get; set; }
}