using SM.Application.ProductFeature.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Queries;

public class GetProductFeatureDetailsQueryValidator : AbstractValidator<GetProductFeatureDetailsQuery>
{
    public GetProductFeatureDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}

