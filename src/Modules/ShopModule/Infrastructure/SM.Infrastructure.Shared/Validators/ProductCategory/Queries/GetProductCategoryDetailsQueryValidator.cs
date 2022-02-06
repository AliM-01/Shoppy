using SM.Application.Contracts.ProductCategory.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Queries;

public class GetProductCategoryDetailsQueryValidator : AbstractValidator<GetProductCategoryDetailsQuery>
{
    public GetProductCategoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

