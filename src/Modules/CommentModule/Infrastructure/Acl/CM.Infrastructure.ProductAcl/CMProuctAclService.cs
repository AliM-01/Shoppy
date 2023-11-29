using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using CM.Application.Sevices;
using SM.Domain.Product;
using SM.Infrastructure.RepositoryExtensions;

namespace CM.Infrastructure.ProductAcl;

public class CMProuctAclService : ICMProductAcl
{
    #region ctor

    private readonly IRepository<Product> _productRepository;

    public CMProuctAclService(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    #endregion

    #region ExistsProduct

    public async Task<bool> ExistsProduct(string productId)
    {
        return await _productRepository.ExistsProduct(productId);
    }

    #endregion

    #region GetProductTitle

    public async Task<string> GetProductTitle(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _productRepository.GetProductTitle(productId);
    }

    #endregion
}
