using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public class DefineColleagueDiscountCommand : IRequest<Response<string>>
{
    public DefineColleagueDiscountCommand(DefineColleagueDiscountDto colleagueDiscount)
    {
        ColleagueDiscount = colleagueDiscount;
    }

    public DefineColleagueDiscountDto ColleagueDiscount { get; set; }
}