using SM.Application.Contracts.ProductCategory.Commands;

namespace SM.Application.ProductCategory.CommandHandles;

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;

    public DeleteProductCategoryCommandHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.FindByIdAsync(request.ProductCategoryId);

        if (productCategory is null)
            throw new NotFoundApiException();

        await _productCategoryRepository.DeleteAsync(productCategory.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}