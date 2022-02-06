using SM.Application.Contracts.ProductFeature.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Queries;

public class FilterProductFeatureQueryValidator : AbstractValidator<FilterProductFeaturesQuery>
{
    public FilterProductFeatureQueryValidator()
    {
        RuleFor(p => p.Filter.ProductId)
            .RangeValidator("شناسه محصول", 1, 10000);
    }
}
