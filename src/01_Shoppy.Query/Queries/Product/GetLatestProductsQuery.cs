using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;

namespace _01_Shoppy.Query.Queries.Product;

public record GetLatestProductsQuery() : IRequest<Response<List<ProductQueryModel>>>;

public class GetLatestProductsQueryHandler : IRequestHandler<GetLatestProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetLatestProductsQueryHandler(
        IGenericRepository<SM.Domain.Product.Product> productRepository, IProductHelper productHelper, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetLatestProductsQuery request, CancellationToken cancellationToken)
    {
        var latestProducts = (await _productRepository.AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.CreationDate)
               .Take(6)
               .ToListAsyncSafe())
               .Select(x => _productHelper.MapProducts<ProductQueryModel>(x).Result)
               .ToList();

        return new Response<List<ProductQueryModel>>(latestProducts);
    }
}
