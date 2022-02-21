namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record RemoveColleagueDiscountCommand(string ColleagueDiscountId) : IRequest<Response<string>>;