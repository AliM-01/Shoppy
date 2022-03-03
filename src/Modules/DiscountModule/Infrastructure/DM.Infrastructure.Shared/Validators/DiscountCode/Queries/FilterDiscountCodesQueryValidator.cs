using DM.Application.Contracts.DiscountCode.Queries;

namespace DM.Infrastructure.Shared.Validators.DiscountCode;

internal class FilterDiscountCodesQueryValidator : AbstractValidator<FilterDiscountCodesQuery>
{
    public FilterDiscountCodesQueryValidator()
    {
        RuleFor(p => p.Filter.Phrase)
            .RequiredValidator("جستجو")
            .MaxLengthValidator("عنوان محصول", 100);
    }
}