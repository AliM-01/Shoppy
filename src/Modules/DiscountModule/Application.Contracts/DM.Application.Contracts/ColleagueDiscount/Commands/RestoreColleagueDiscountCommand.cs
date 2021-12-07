namespace DM.Application.Contracts.ColleagueDiscount.Commands;
public class RestoreColleagueDiscountCommand : IRequest<Response<string>>
{
    public RestoreColleagueDiscountCommand(long colleagueDiscountId)
    {
        ColleagueDiscountId = colleagueDiscountId;
    }

    public long ColleagueDiscountId { get; set; }
}