using DM.Application.Contracts.ColleagueDiscount.DTOs;

namespace DM.Application.Contracts.ColleagueDiscount.Commands;

public class EditColleagueDiscountCommand : IRequest<Response<string>>
{
    public EditColleagueDiscountCommand(EditColleagueDiscountDto colleagueDiscount)
    {
        ColleagueDiscount = colleagueDiscount;
    }

    public EditColleagueDiscountDto ColleagueDiscount { get; set; }
}