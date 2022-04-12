using SM.Application.Contracts.Product.Commands;

namespace SM.Application.ProductCategory.CommandHandles;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.Product.Product> _productRepository;

    public DeleteProductCommandHandler(IRepository<Domain.Product.Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        await _productRepository.DeleteAsync(product.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}