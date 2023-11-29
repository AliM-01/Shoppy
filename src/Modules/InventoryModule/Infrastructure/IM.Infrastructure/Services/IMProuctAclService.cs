using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using IM.Application.Sevices;
using SM.Domain.Product;
using SM.Infrastructure.RepositoryExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IM.Infrastructure.ProductAcl;

public class IMProuctAclService : IIMProuctAclService
{
    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<Product> _productRepository;

    public IMProuctAclService(IRepository<Domain.Inventory.Inventory> InventoryRepository,
         IRepository<Product> productRepository)
    {
        _inventoryRepository = Guard.Against.Null(InventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    public async Task<bool> ExistsProduct(string productId)
    {
        return await _productRepository.ExistsProduct(productId);
    }
    public async Task<bool> ExistsInventory(string productId)
    {
        if (!(await ExistsProduct(productId)))
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        return await _inventoryRepository.ExistsAsync(x => x.ProductId == productId);
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

    Task<HashSet<string>> IIMProuctAclService.FilterTitle(string filter)
    {
        throw new System.NotImplementedException();
    }
}
