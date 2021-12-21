namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record DeleteColleagueDiscountCommand(long ColleagueDiscountId) : IRequest<Response<string>>;