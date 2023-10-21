using SM.Application.ProductFeature.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Queries;

public class FilterProductFeatureQueryValidator : AbstractValidator<FilterProductFeaturesQuery>
{
    public FilterProductFeatureQueryValidator()
    {
        RuleFor(p => p.Filter.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}
