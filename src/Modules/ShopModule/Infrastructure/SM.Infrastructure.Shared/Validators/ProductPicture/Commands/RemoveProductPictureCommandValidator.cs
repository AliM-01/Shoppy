using SM.Application.Contracts.ProductPicture.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductPicture.Commands;

public class RemoveProductPictureCommandValidator : AbstractValidator<RemoveProductPictureCommand>
{
    public RemoveProductPictureCommandValidator()
    {
        RuleFor(p => p.ProductPictureId)
            .RequiredValidator("شناسه");
    }
}
