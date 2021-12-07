namespace DM.Application.Contracts.ColleagueDiscount.Commands;
public class RemoveColleagueDiscountCommand : IRequest<Response<string>>
{
    public RemoveColleagueDiscountCommand(long colleagueDiscountId)
    {
        ColleagueDiscountId = colleagueDiscountId;
    }

    public long ColleagueDiscountId { get; set; }
}