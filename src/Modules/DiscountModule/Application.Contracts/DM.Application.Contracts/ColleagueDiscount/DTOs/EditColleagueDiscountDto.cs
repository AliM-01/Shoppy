namespace DM.Application.Contracts.ColleagueDiscount.DTOs;
public class EditColleagueDiscountDto : DefineColleagueDiscountDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
}