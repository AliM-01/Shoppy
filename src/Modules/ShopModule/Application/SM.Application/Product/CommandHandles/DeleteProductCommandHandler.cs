using _0_Framework.Application.Utilities.ImageRelated;
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

        File.Delete(PathExtension.ProductImage + product.ImagePath);
        File.Delete(PathExtension.ProductThumbnailImage + product.ImagePath);

        await _productRepository.SoftDelete(product.Id);
        await _productRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}