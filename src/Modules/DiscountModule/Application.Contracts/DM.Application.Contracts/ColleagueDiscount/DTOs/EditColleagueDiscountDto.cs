namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class EditColleagueDiscountDto : DefineColleagueDiscountDto
{
    [JsonProperty("id")]
    public long Id { get; set; }
}