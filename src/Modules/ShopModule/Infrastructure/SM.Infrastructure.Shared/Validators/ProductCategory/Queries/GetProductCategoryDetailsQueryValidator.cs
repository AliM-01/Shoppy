using SM.Application.ProductCategory.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Queries;

public class GetProductCategoryDetailsQueryValidator : AbstractValidator<GetProductCategoryDetailsQuery>
{
    public GetProductCategoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}

