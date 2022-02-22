namespace DM.Application.Contracts.ProductDiscount.DTOs;
public class EditProductDiscountDto : DefineProductDiscountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}