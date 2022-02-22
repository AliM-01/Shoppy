using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class CheckProductHasProductDiscountQueryValidator : AbstractValidator<CheckProductHasProductDiscountQuery>
{
    public CheckProductHasProductDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RangeValidator("شناسه محصول", 1, 100000);
    }
}