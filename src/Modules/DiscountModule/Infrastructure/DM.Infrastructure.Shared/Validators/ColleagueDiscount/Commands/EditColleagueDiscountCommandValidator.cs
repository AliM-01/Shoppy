using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class EditColleagueDiscountCommandValidator : AbstractValidator<EditColleagueDiscountCommand>
{
    public EditColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscount.Id)
            .RangeValidator("شناسه تخفیف", 1, 100000);

        RuleFor(p => p.ColleagueDiscount.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);

        RuleFor(p => p.ColleagueDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}