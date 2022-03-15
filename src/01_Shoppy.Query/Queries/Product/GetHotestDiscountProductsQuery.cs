using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;
using DM.Domain.ProductDiscount;

namespace _01_Shoppy.Query.Queries.Product;

public record GetHotestDiscountProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetHotestDiscountProductsQueryHandler : IRequestHandler<GetHotestDiscountProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IGenericRepository<ProductDiscount> _productDiscount;
    private readonly IMapper _mapper;
    private readonly IProductHelper _productHelper;

    public GetHotestDiscountProductsQueryHandler(
        IGenericRepository<SM.Domain.Product.Product> productRepository, IGenericRepository<ProductDiscount> productDiscount,
        IMapper mapper, IProductHelper productHelper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetHotestDiscountProductsQuery request, CancellationToken cancellationToken)
    {
        List<string> hotDiscountRateIds = (await _productDiscount
            .AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(6)
            .ToListAsyncSafe())
            .Select(x => x.ProductId).ToList();

        var products = (await _productRepository.AsQueryable()
               .Where(x => hotDiscountRateIds.Contains(x.Id))
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(6)
               .ToListAsyncSafe())
               .Select(x => _productHelper.MapProducts<ProductQueryModel>(x, true).Result)
               .ToList(); ;

        return new Response<List<ProductQueryModel>>(products);
    }
}
