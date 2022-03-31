using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using IM.Application.Contracts.Sevices;
using SM.Domain.Product;
using SM.Infrastructure.Shared.RepositoryExtensions;

namespace IM.Infrastructure.ProductAcl;
public class IMProuctAclService : IIMProuctAclService
{
    #region ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<Product> _productRepository;

    public IMProuctAclService(IRepository<Domain.Inventory.Inventory> InventoryRepository,
         IRepository<Product> productRepository)
    {
        _inventoryRepository = Guard.Against.Null(InventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    #endregion

    #region ExistsProduct

    public async Task<bool> ExistsProduct(string productId)
    {
        return await _productRepository.ExistsProduct(productId);
    }

    #endregion

    #region ExistsInventory

    public async Task<bool> ExistsInventory(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _inventoryRepository.ExistsAsync(x => x.ProductId == productId);
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
