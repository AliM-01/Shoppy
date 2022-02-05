using DM.Application.Contracts.ColleagueDiscount.Queries;

namespace DM.Infrastructure.Shared.Validators.ColleagueDiscount;

internal class FilterColleagueDiscountsQueryValidator : AbstractValidator<FilterColleagueDiscountsQuery>
{
    public FilterColleagueDiscountsQueryValidator()
    {
        RuleFor(p => p.Filter.ProductTitle)
            .RequiredValidator("عنوان محصول")
            .MaxLengthValidator("عنوان محصول", 100);

        RuleFor(p => p.Filter.ProductId)
            .RangeValidator("شناسه محصول", 0, 10000);
    }
}