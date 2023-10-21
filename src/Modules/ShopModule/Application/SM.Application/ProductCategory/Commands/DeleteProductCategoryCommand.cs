using _0_Framework.Domain.Validators;
using FluentValidation;

namespace SM.Application.ProductCategory.Commands;

public record DeleteProductCategoryCommand(string ProductCategoryId) : IRequest<ApiResult>;

public class DeleteProductCategoryCommandValidator : AbstractValidator<DeleteProductCategoryCommand>
{
    public DeleteProductCategoryCommandValidator()
    {
        RuleFor(p => p.ProductCategoryId)
            .RequiredValidator("شناسه دسته بندی");
    }
}


public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;

    public DeleteProductCategoryCommandHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
    }

    public async Task<ApiResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.FindByIdAsync(request.ProductCategoryId);

        NotFoundApiException.ThrowIfNull(productCategory);

        await _productCategoryRepository.DeleteAsync(productCategory.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}