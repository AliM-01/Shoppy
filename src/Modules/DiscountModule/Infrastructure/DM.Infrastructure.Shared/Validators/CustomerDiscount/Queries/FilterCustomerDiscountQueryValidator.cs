using DM.Application.Contracts.CustomerDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.CustomerDiscount;

internal class FilterCustomerDiscountsQueryValidator : AbstractValidator<FilterCustomerDiscountsQuery>
{
    public FilterCustomerDiscountsQueryValidator()
    {
        RuleFor(p => p.Filter.ProductTitle)
            .RequiredValidator("عنوان محصول")
            .MaxLengthValidator("عنوان محصول", 100);
    }
}