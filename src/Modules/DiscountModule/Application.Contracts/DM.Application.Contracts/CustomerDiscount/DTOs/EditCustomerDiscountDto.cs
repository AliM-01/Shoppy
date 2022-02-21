namespace DM.Application.Contracts.CustomerDiscount.DTOs;
public class EditCustomerDiscountDto : DefineCustomerDiscountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}