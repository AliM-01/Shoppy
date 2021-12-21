namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record RestoreColleagueDiscountCommand(long ColleagueDiscountId) : IRequest<Response<string>>;