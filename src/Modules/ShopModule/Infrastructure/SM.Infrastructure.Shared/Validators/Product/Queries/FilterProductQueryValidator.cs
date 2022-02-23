using SM.Application.Contracts.Product.Queries;

namespace SM.Infrastructure.Shared.Validators.Product.Queries;

internal class FilterProductsQueryValidator : AbstractValidator<FilterProductsQuery>
{
    public FilterProductsQueryValidator()
    {
        RuleFor(p => p.Filter.Search)
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.Filter.CategoryId)
            .RequiredValidator("شناسه دسته بندی");
    }
}
