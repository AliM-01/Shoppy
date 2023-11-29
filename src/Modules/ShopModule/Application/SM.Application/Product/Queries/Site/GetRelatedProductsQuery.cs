using SM.Application.Product.DTOs.Site;
using SM.Application.Services;

namespace SM.Application.Product.Queries.Site;

public record GetRelatedProductsQuery(string CategoryId) : IRequest<IEnumerable<ProductSiteDto>>;

public class GetRelatedProductsQueryHandler : IRequestHandler<GetRelatedProductsQuery, IEnumerable<ProductSiteDto>>
{
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

    public Task<IEnumerable<ProductSiteDto>> Handle(GetRelatedProductsQuery request, CancellationToken cancellationToken)
    {
        var relatedArticles = _productRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.CreationDate)
               .Where(x => x.CategoryId == request.CategoryId)
               .Take(5)
               .ToList()
               .Select(x => _productHelper.MapProducts<ProductSiteDto>(x).Result);

        return Task.FromResult(relatedArticles);
    }
}
