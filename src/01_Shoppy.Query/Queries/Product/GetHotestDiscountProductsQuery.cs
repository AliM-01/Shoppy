using _01_Shoppy.Query.Helpers.Product;
using DM.Domain.ProductDiscount;

namespace _01_Shoppy.Query.Queries.Product;

public record GetHotestDiscountProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetHotestDiscountProductsQueryHandler : IRequestHandler<GetHotestDiscountProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IRepository<ProductDiscount> _productDiscount;
    private readonly IMapper _mapper;
    private readonly IProductHelper _productHelper;

    public GetHotestDiscountProductsQueryHandler(
        IRepository<SM.Domain.Product.Product> productRepository, IRepository<ProductDiscount> productDiscount,
        IMapper mapper, IProductHelper productHelper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<Response<List<ProductQueryModel>>> Handle(GetHotestDiscountProductsQuery request, CancellationToken cancellationToken)
    {
        List<string> hotDiscountRateIds = _productDiscount
            .AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(6)
            .ToList()
            .Select(x => x.ProductId)
            .ToList();

        var products = _productRepository.AsQueryable()
                                         .Where(x => hotDiscountRateIds.Contains(x.Id))
                                         .OrderByDescending(x => x.LastUpdateDate)
                                         .Take(6)
                                         .ToList()
                                         .Select(x => _productHelper.MapProducts<ProductQueryModel>(x, true).Result)
                                         .ToList(); ;

        return Task.FromResult(new Response<List<ProductQueryModel>>(products));
    }
}
