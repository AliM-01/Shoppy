using SM.Application.Contracts.Product.Queries;

namespace SM.Application.Product.CommandHandles;

public class UpdateProductIsInStockCommandHandler : IRequestHandler<UpdateProductIsInStockCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Product.Product> _productRepository;

    public UpdateProductIsInStockCommandHandler(IGenericRepository<Domain.Product.Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(UpdateProductIsInStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        product.IsInStock = true;
        _productRepository.Update(product);

        await _productRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}
