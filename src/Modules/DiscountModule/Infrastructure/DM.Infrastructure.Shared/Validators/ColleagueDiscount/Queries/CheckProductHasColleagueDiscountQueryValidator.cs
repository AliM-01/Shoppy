using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class CheckProductHasColleagueDiscountQueryValidator : AbstractValidator<CheckProductHasColleagueDiscountQuery>
{
    public CheckProductHasColleagueDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);
    }
}