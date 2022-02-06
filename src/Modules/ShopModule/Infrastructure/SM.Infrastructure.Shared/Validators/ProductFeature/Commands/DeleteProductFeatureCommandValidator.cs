using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Commands;

public class DeleteProductFeatureCommandValidator : AbstractValidator<DeleteProductFeatureCommand>
{
    public DeleteProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeatureId)
            .RangeValidator("شناسه دسته بندی", 1, 100000);
    }
}
