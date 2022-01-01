using _0_Framework.Application.Extensions;
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

    public async Task<Response<List<ProductQueryModel>>> GetLatestProducts()
    {
        #region all inventories query

        var inventories = await _inventoryContext.Inventory.AsQueryable()
            .Select(x => new
            {
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToListAsync();

        #endregion

        #region all discounts query

        var discounts = await _discountContext.CustomerDiscounts.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.Rate
            }).ToListAsync();

        #endregion

        #region all categories query

        var categories = await _shopContext.ProductCategories.AsQueryable()
            .Select(x => new
            {
                x.Id,
                x.Title
            }).ToListAsync();

        #endregion

        var latestProducts = await _shopContext.Products
               .Include(x => x.Category)
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();


        latestProducts.ForEach(product =>
        {
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId).Title.ToString();
            if (inventories.Any(x => x.ProductId == product.Id))
            {
                // calculate unitPrice
                double unitPrice = inventories.FirstOrDefault(x => x.ProductId == product.Id).UnitPrice;
                product.Price = unitPrice.ToMoney();

                if (discounts.Any(x => x.ProductId == product.Id))
                {
                    // calculate discountRate
                    int discountRate = discounts.FirstOrDefault(x => x.ProductId == product.Id).Rate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    // calculate PriceWithDiscount
                    var discountAmount = Math.Round((unitPrice * discountRate / 100));
                    product.PriceWithDiscount = (unitPrice - discountAmount).ToMoney();
                }
            }

        });

        return new Response<List<ProductQueryModel>>(latestProducts);
    }
}
