using SM.Application.ProductFeature.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductFeature.Commands;

public class EditProductFeatureCommandValidator : AbstractValidator<EditProductFeatureCommand>
{
    public EditProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeature.Id)
            .RequiredValidator("شناسه");

        RuleFor(p => p.ProductFeature.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ProductFeature.FeatureTitle)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductFeature.FeatureValue)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}

