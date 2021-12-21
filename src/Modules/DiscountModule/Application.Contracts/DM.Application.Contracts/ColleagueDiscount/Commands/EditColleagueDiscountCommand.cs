using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record EditColleagueDiscountCommand
    (EditColleagueDiscountDto ColleagueDiscount) : IRequest<Response<string>>;