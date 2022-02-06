using SM.Application.Contracts.ProductFeature.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Queries;

public class GetProductFeatureDetailsQueryValidator : AbstractValidator<GetProductFeatureDetailsQuery>
{
    public GetProductFeatureDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RangeValidator("شناسه", 1, 100000);
    }
}

