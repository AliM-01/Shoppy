using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class RestoreColleagueDiscountCommandValidator : AbstractValidator<RestoreColleagueDiscountCommand>
{
    public RestoreColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscountId)
            .RangeValidator("شناسه تخفیف", 1, 100000);
    }
}