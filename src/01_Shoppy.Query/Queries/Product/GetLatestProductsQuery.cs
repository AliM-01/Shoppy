using _01_Shoppy.Query.Helpers.Product;

namespace _01_Shoppy.Query.Queries.Product;

public record GetLatestProductsQuery() : IRequest<ApiResult<List<ProductQueryModel>>>;

public class GetLatestProductsQueryHandler : IRequestHandler<GetLatestProductsQuery, ApiResult<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetLatestProductsQueryHandler(
        IRepository<SM.Domain.Product.Product> productRepository, IProductHelper productHelper, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<ApiResult<List<ProductQueryModel>>> Handle(GetLatestProductsQuery request, CancellationToken cancellationToken)
    {
        var latestProducts = _productRepository.AsQueryable(cancellationToken: cancellationToken)
                                                .OrderByDescending(x => x.CreationDate)
                                                .Take(6)
                                                .ToList()
                                                .Select(x => _productHelper.MapProducts<ProductQueryModel>(x).Result)
                                                .ToList();

        return Task.FromResult(ApiResponse.Success<List<ProductQueryModel>>(latestProducts));
    }
}
