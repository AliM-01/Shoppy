using SM.Application.Contracts.Product.Commands;

namespace SM.Infrastructure.Shared.Validators.Product.Commands;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.ProductId)
            .RangeValidator("شناسه", 1, 100000);
    }
}
