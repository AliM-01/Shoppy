using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using DM.Application.Contracts.Sevices;
using SM.Domain.Product;
using SM.Infrastructure.RepositoryExtensions;

namespace DM.Infrastructure.ProductAcl;
public class DMProucAclService : IDMProucAclService
{
    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IRepository<Product> _productRepository;

    public DMProucAclService(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IRepository<Product> productRepository)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    public async Task<bool> ExistsProduct(string productId)
    {
        return await _productRepository.ExistsProduct(productId);
    }

    public async Task<bool> ExistsProductDiscount(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _productDiscountRepository.ExistsAsync(x => x.ProductId == productId);
    }

    public async Task<string> GetProductTitle(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _productRepository.GetProductTitle(productId);
    }

    public async Task<HashSet<string>> FilterTitle(string filter)
    {
        return await _productRepository.FullTextSearch(x => x.Title, filter);
    }
}
