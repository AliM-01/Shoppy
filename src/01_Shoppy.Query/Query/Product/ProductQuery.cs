using _01_Shoppy.Query.Contracts.Product;
using AutoMapper;
using DM.Infrastructure.Persistence.Context;
using IM.Infrastructure.Persistence.Context;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class ProductQuery : IProductQuery
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly DiscountDbContext _discountContext;
    private readonly InventoryDbContext _inventoryContext;
    private readonly IMapper _mapper;

    public ProductQuery(
        ShopDbContext shopContext, DiscountDbContext discountContext,
        InventoryDbContext inventoryContext, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _discountContext = Guard.Against.Null(discountContext, nameof(_discountContext));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_discountContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductQueryModel>>> GetLatestProducts()
    {
        var latestProducts = await _shopContext.Products
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();

        var inventories = await _inventoryContext.Inventory.AsQueryable()
            .Select(x => new
            {
                x.Id,
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToListAsync();

        latestProducts.ForEach(product =>
        {
            product.Price = inventories.FirstOrDefault(x => x.Id == product.Id).UnitPrice.ToString("#,0");
        });

        return new Response<IEnumerable<ProductQueryModel>>(latestProducts);
    }
}
