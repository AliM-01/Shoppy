using _01_Shoppy.Query.Queries.Discount;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class CheckDiscountCodeQueryValidator : AbstractValidator<CheckDiscountCodeQuery>
{
    public CheckDiscountCodeQueryValidator()
    {
        RuleFor(p => p.Code)
            .RequiredValidator("جستجو");
    }
}