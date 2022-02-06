using SM.Application.Contracts.Product.Queries;

namespace SM.Infrastructure.Shared.Validators.Product.Queries;

public class ExistsProductIdQueryValidator : AbstractValidator<ExistsProductIdQuery>
{
    public ExistsProductIdQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RangeValidator("شناسه", 1, 100000);
    }
}

