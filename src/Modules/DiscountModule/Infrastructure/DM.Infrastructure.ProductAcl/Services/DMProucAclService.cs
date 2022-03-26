using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using DM.Application.Contracts.Sevices;
using SM.Domain.Product;

namespace DM.Infrastructure.ProductAcl.Services;
public class DMProucAclService : IDMProucAclService
{
    #region ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IRepository<Product> _productRepository;

    public DMProucAclService(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IRepository<Product> productRepository)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    #endregion

    #region ExistsProduct

    public async Task<bool> ExistsProduct(string productId)
    {
        return await _productRepository.ExistsAsync(p => p.Id == productId);
    }

    #endregion

    #region ExistsProductDiscount

    public async Task<bool> ExistsProductDiscount(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _productDiscountRepository.ExistsAsync(x => x.ProductId == productId);
    }

    #endregion

    #region GetProductTitle

    public async Task<string> GetProductTitle(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return (await _productRepository.GetByIdAsync(productId)).Title;
    }

    #endregion

    #region Filter Title

    public async Task<HashSet<string>> FilterTitle(string filter)
    {
        var products = await _productRepository.AsQueryable()
           .Select(x => new
           {
               x.Id,
               x.Title
           }).ToListAsyncSafe();

        var ids = await _productRepository.AsQueryable()
                .Where(s => s.Title.Contains(filter))
                .Select(x => x.Id).ToListAsyncSafe();

        return ids.ToHashSet();
    }

    #endregion
}
