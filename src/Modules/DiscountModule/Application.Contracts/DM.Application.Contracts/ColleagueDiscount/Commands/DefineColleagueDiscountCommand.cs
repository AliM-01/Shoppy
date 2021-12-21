using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public record DefineColleagueDiscountCommand
    (DefineColleagueDiscountDto ColleagueDiscount) : IRequest<Response<string>>;
