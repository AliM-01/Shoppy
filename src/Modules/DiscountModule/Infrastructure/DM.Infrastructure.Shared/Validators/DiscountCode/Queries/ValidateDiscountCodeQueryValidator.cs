using _01_Shoppy.Query.Queries.Discount;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

public class ValidateDiscountCodeQueryValidator : AbstractValidator<ValidateDiscountCodeQuery>
{
    public ValidateDiscountCodeQueryValidator()
    {
        RuleFor(p => p.Code)
            .RequiredValidator("جستجو");
    }
}