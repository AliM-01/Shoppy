using _0_Framework.Application.Extensions;
using AutoMapper;
using DM.Infrastructure.Persistence.Context;
using IM.Infrastructure.Persistence.Context;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Helpers.Product;

public class ProductHelper : IProductHelper
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly DiscountDbContext _discountContext;
    private readonly InventoryDbContext _inventoryContext;
    private readonly IMapper _mapper;

    public ProductHelper(
        ShopDbContext shopContext, DiscountDbContext discountContext,
        InventoryDbContext inventoryContext, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _discountContext = Guard.Against.Null(discountContext, nameof(_discountContext));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_discountContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    #region MapProductsFromProductCategories

    public async Task<List<ProductQueryModel>> MapProductsFromProductCategories(List<SM.Domain.Product.Product> products)
    {
        var mappedProducts = products
               .OrderByDescending(x => x.LastUpdateDate)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel
                   {
                       CategoryId = product.CategoryId.Value
                   }))
               .ToList();

        return await MapProducts(mappedProducts);
    }

    #endregion

    #region MapProducts

    public async Task<List<ProductQueryModel>> MapProducts
        (List<ProductQueryModel> products, bool hotDiscountQuery = false)
    {
        #region all discounts query

        var discounts = await _discountContext.CustomerDiscounts.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.Rate
            }).ToListAsync();

        if (hotDiscountQuery)
        {
            discounts = discounts.Where(x => x.Rate >= 25).ToList();
        }

        #endregion

        #region all categories query

        var categories = await _shopContext.ProductCategories.AsQueryable()
            .Select(x => new
            {
                x.Id,
                x.Title
            }).ToListAsync();

        #endregion

        products.ForEach(product =>
        {
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId).Title.ToString();

            (bool existsProductInventory, double productUnitPrice) = GetProductUnitPrice(product.Id).Result;

            if (existsProductInventory)
            {
                // calculate unitPrice
                product.Price = productUnitPrice.ToMoney();
                product.UnitPrice = productUnitPrice;

                if (discounts.Any(x => x.ProductId == product.Id))
                {
                    // calculate discountRate
                    int discountRate = discounts.FirstOrDefault(x => x.ProductId == product.Id).Rate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    // calculate PriceWithDiscount
                    var discountAmount = Math.Round((productUnitPrice * discountRate / 100));
                    product.PriceWithDiscount = (productUnitPrice - discountAmount).ToMoney();
                }
            }

        });

        return products;
    }

    #endregion

    #region Get Product UnitPrice

    public async Task<(bool, double)> GetProductUnitPrice(long productId)
    {
        if (await _inventoryContext.Inventory.AnyAsync(x => x.ProductId == productId))
            return (false, default);

        return (true, await _inventoryContext.Inventory
            .Where(x => x.ProductId == productId)
            .Select(x => x.UnitPrice).FirstOrDefaultAsync());
    }

    #endregion
}