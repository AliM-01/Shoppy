using SM.Application.Contracts.ProductCategory.Commands;

namespace SM.Infrastructure.Shared.Validators.ProductCategory.Commands;

public class DeleteProductCategoryCommandValidator : AbstractValidator<DeleteProductCategoryCommand>
{
    public DeleteProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategoryId)
            .RequiredValidator("شناسه دسته بندی");
    }
}
