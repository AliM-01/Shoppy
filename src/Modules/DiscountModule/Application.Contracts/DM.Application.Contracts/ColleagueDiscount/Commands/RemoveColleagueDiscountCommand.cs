namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record RemoveColleagueDiscountCommand(long ColleagueDiscountId) : IRequest<Response<string>>;