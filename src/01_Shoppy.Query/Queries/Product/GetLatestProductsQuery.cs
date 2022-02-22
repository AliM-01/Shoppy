using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Product;

public record GetLatestProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetLatestProductsQueryHandler : IRequestHandler<GetLatestProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetLatestProductsQueryHandler(
        ShopDbContext shopContext, IProductHelper productHelper, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetLatestProductsQuery request, CancellationToken cancellationToken)
    {
        var latestProducts = await _shopContext.Products
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();

        var returnData = new List<ProductQueryModel>();

        latestProducts.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProducts<ProductQueryModel>(p).Result;
            returnData.Add(mappedProduct);
        });

        return new Response<List<ProductQueryModel>>(returnData);
    }
}
