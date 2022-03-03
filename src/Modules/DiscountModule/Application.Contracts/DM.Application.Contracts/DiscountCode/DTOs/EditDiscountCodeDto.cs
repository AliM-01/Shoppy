namespace DM.Application.Contracts.DiscountCode.DTOs;
public class EditDiscountCodeDto : DefineDiscountCodeDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}