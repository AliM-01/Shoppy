namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record RestoreColleagueDiscountCommand(string ColleagueDiscountId) : IRequest<Response<string>>;