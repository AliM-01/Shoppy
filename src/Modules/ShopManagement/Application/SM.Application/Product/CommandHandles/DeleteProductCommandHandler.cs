using SM.Application.Contracts.Product.Commands;
using System.IO;

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
        var product = await _productRepository.GetEntityById(request.ProductId);

        if (product is null)
            throw new NotFoundApiException();

        File.Delete($"wwwroot/product/original/{product.ImagePath}");
        File.Delete($"wwwroot/product/thumbnail/{product.ImagePath}");

        await _productRepository.SoftDelete(product.Id);
        await _productRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}