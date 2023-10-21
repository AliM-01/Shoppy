using SM.Application.ProductPicture.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductPicture.Commands;

public class CreateProductPictureCommandValidator : AbstractValidator<CreateProductPictureCommand>
{
    public CreateProductPictureCommandValidator()
    {
        RuleFor(p => p.ProductPicture.ProductId)
             .RequiredValidator("شناسه محصول");
    }
}