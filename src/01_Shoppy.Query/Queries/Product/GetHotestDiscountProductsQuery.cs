using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using DM.Domain.ProductDiscount;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Product;

public record GetHotestDiscountProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetHotestDiscountProductsQueryHandler : IRequestHandler<GetHotestDiscountProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly IMongoHelper<ProductDiscount> _productDiscount;
    private readonly IMapper _mapper;
    private readonly IProductHelper _productHelper;

    public GetHotestDiscountProductsQueryHandler(
        ShopDbContext shopContext, IMongoHelper<ProductDiscount> productDiscount,
        IMapper mapper, IProductHelper productHelper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetHotestDiscountProductsQuery request, CancellationToken cancellationToken)
    {
        List<long> hotDiscountRateIds = (await _productDiscount
            .AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(8)
            .ToListAsyncSafe())
            .Select(x => x.ProductId).
            ToList();

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
