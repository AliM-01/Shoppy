namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record DeleteColleagueDiscountCommand(string ColleagueDiscountId) : IRequest<Response<string>>;