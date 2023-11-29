using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using IM.Domain.Inventory;
using SM.Application.Services;

namespace SM.Infrastructure.AclServices;

public class InventoryAclService : IInventoryAclService
{
    private readonly IRepository<Inventory> _inventoryRepository;

    public InventoryAclService(IRepository<Inventory> inventoryRepository)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
    }

    public decimal GetMaxPrice()
    {
        decimal maxPrice = _inventoryRepository.AsQueryable().OrderByDescending(x => x.UnitPrice)
            .Select(x => x.UnitPrice).FirstOrDefault();

        return maxPrice;
    }
}
