using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.Product;
using AutoMapper;
using DM.Infrastructure.Persistence.Context;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Product;

public record GetHotestDiscountProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetHotestDiscountProductsQueryHandler : IRequestHandler<GetHotestDiscountProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly DiscountDbContext _discountContext;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetHotestDiscountProductsQueryHandler(
        ShopDbContext shopContext, DiscountDbContext discountContext, IProductHelper productHelper, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _discountContext = Guard.Against.Null(discountContext, nameof(_discountContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetHotestDiscountProductsQuery request, CancellationToken cancellationToken)
    {
        List<long> hotDiscountRateIds = await _discountContext.ProductDiscounts.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(8)
            .Select(x => x.ProductId).ToListAsync();

        var products = await _shopContext.Products
               .Include(x => x.Category)
               .Where(x => hotDiscountRateIds.Contains(x.Id))
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();

        var returnData = new List<ProductQueryModel>();

        products.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProducts<ProductQueryModel>(p, true).Result;
            returnData.Add(mappedProduct);
        });

        return new Response<List<ProductQueryModel>>(returnData);
    }
}
