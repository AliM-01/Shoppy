using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class EditColleagueDiscountCommandValidator : AbstractValidator<EditColleagueDiscountCommand>
{
    public EditColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscount.Id)
            .RequiredValidator("شناسه تخفیف");

        RuleFor(p => p.ColleagueDiscount.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ColleagueDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}