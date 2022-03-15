using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;

namespace _01_Shoppy.Query.Queries.Product;

public record GetRelatedProductsQuery(string CategoryId) : IRequest<Response<List<ProductQueryModel>>>;

public class GetRelatedProductsQueryHandler : IRequestHandler<GetRelatedProductsQuery, Response<List<ProductQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetRelatedProductsQueryHandler(
        IGenericRepository<SM.Domain.Product.Product> productRepository, IProductHelper productHelper, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<ProductQueryModel>>> Handle(GetRelatedProductsQuery request, CancellationToken cancellationToken)
    {
        var relatedArticles =
            (await _productRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.CreationDate)
               .Where(x => x.CategoryId == request.CategoryId)
               .Take(5)
               .ToListAsyncSafe())
               .Select(x => _productHelper.MapProducts<ProductQueryModel>(x).Result)
               .ToList();

        return new Response<List<ProductQueryModel>>(relatedArticles);
    }
}
