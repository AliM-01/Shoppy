using SM.Application.Contracts.ProductPicture.Queries;

namespace SM.Infrastructure.Shared.Validators.ProductPicture.Queries;

public class GetProductPicturesQueryValidator : AbstractValidator<GetProductPicturesQuery>
{
    public GetProductPicturesQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه محصول");
    }
}
