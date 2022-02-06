using SM.Application.Contracts.ProductPicture.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductPicture.Commands;

public class CreateProductPictureCommandValidator : AbstractValidator<CreateProductPictureCommand>
{
    public CreateProductPictureCommandValidator()
    {
        RuleFor(p => p.ProductPicture.ProductId)
             .RangeValidator("شناسه محصول", 1, 10000);
    }
}