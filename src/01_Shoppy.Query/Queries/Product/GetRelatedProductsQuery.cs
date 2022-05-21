using _01_Shoppy.Query.Helpers.Product;

namespace _01_Shoppy.Query.Queries.Product;

public record GetRelatedProductsQuery(string CategoryId) : IRequest<IEnumerable<ProductQueryModel>>;

public class GetRelatedProductsQueryHandler : IRequestHandler<GetRelatedProductsQuery, IEnumerable<ProductQueryModel>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetRelatedProductsQueryHandler(
        IRepository<SM.Domain.Product.Product> productRepository, IProductHelper productHelper, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<IEnumerable<ProductQueryModel>> Handle(GetRelatedProductsQuery request, CancellationToken cancellationToken)
    {
        var relatedArticles = _productRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.CreationDate)
               .Where(x => x.CategoryId == request.CategoryId)
               .Take(5)
               .ToList()
               .Select(x => _productHelper.MapProducts<ProductQueryModel>(x).Result);

        return Task.FromResult(relatedArticles);
    }
}
