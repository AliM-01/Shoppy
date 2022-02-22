using DM.Application.Contracts.ProductDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ProductDiscount;

internal class FilterProductDiscountsQueryValidator : AbstractValidator<FilterProductDiscountsQuery>
{
    public FilterProductDiscountsQueryValidator()
    {
        RuleFor(p => p.Filter.ProductTitle)
            .RequiredValidator("عنوان محصول")
            .MaxLengthValidator("عنوان محصول", 100);
    }
}