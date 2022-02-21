using DM.Application.Contracts.CustomerDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

public class CheckProductHasCustomerDiscountQueryValidator : AbstractValidator<CheckProductHasCustomerDiscountQuery>
{
    public CheckProductHasCustomerDiscountQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}