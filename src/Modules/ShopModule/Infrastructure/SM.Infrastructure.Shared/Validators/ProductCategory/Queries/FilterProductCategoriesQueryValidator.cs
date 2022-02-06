using SM.Application.Contracts.ProductCategory.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Queries;

internal class FilterProductCategoriesQueryValidator : AbstractValidator<FilterProductCategoriesQuery>
{
    public FilterProductCategoriesQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);
    }
}
