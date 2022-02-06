using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Commands;

public class CreateProductFeatureCommandValidator : AbstractValidator<CreateProductFeatureCommand>
{
    public CreateProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeature.ProductId)
            .RangeValidator("شناسه محصول", 1, 10000);

        RuleFor(p => p.ProductFeature.FeatureTitle)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductFeature.FeatureValue)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}

