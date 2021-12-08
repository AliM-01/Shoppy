namespace DM.Application.Contracts.ColleagueDiscount.Commands;
public class DeleteColleagueDiscountCommand : IRequest<Response<string>>
{
    public DeleteColleagueDiscountCommand(long colleagueDiscountId)
    {
        ColleagueDiscountId = colleagueDiscountId;
    }

    public long ColleagueDiscountId { get; set; }
}