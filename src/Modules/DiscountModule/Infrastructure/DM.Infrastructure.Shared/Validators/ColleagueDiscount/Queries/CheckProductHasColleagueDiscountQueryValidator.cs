using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

public class CheckProductHasColleagueDiscountQueryValidator : AbstractValidator<CheckProductHasColleagueDiscountQuery>
{
    public CheckProductHasColleagueDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}