using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class RemoveColleagueDiscountCommandValidator : AbstractValidator<RemoveColleagueDiscountCommand>
{
    public RemoveColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscountId)
            .RangeValidator("شناسه تخفیف", 1, 100000);
    }
}