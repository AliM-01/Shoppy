namespace DM.Application.Contracts.ColleagueDiscount.DTOs;

public class CheckProductHasColleagueDiscountResponseDto
{
    [JsonProperty("existsColleagueDiscount")]
    public bool ExistsColleagueDiscount { get; set; }
}
