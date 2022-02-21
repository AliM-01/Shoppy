using DM.Application.Contracts.ColleagueDiscount.Commands;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class DefineColleagueDiscountCommandValidator : AbstractValidator<DefineColleagueDiscountCommand>
{
    public DefineColleagueDiscountCommandValidator()
    {
        RuleFor(p => p.ColleagueDiscount.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ColleagueDiscount.Rate)
            .RangeValidator("درصد", 1, 100);
    }
}