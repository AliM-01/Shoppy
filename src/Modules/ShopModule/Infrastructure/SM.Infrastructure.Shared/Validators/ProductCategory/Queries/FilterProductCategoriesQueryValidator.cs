using SM.Application.ProductCategory.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Queries;

public class FilterProductCategoriesQueryValidator : AbstractValidator<FilterProductCategoriesQuery>
{
    public FilterProductCategoriesQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);
    }
}
