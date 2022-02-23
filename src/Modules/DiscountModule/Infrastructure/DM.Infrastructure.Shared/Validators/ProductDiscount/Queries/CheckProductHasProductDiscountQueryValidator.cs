using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

public class CheckProductHasProductDiscountQueryValidator : AbstractValidator<CheckProductHasProductDiscountQuery>
{
    public CheckProductHasProductDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}