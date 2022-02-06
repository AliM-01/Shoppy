using SM.Application.Contracts.Product.Queries;

namespace SM.Infrastructure.Shared.Validators.Product.Queries;

public class GetProductDetailsQueryValidator : AbstractValidator<GetProductDetailsQuery>
{
    public GetProductDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

