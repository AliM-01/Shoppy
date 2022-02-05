using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class DeleteColleagueDiscountCommandValidator : AbstractValidator<DeleteColleagueDiscountCommand>
{
    public DeleteColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscountId)
            .RangeValidator("شناسه تخفیف", 1, 100000);
    }
}