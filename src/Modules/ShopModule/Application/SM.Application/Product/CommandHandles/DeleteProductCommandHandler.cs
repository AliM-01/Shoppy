using SM.Application.Contracts.Product.Commands;

namespace SM.Application.ProductCategory.CommandHandles;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;

    public DeleteProductCommandHandler(IGenericRepository<Domain.Product.Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        await _productRepository.DeleteAsync(product.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}