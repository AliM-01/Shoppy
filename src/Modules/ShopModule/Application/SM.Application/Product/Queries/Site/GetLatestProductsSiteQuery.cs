using SM.Application.Product.DTOs.Site;
using SM.Application.Services;

namespace SM.Application.Product.Queries.Site;

public record GetLatestProductsSiteQuery() : IRequest<IEnumerable<ProductSiteDto>>;

public class GetLatestProductsSiteQueryHandler : IRequestHandler<GetLatestProductsSiteQuery, IEnumerable<ProductSiteDto>>
{
    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetLatestProductsSiteQueryHandler(
        IRepository<SM.Domain.Product.Product> productRepository, IProductHelper productHelper, IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<ProductSiteDto>> Handle(GetLatestProductsSiteQuery request, CancellationToken cancellationToken)
    {
        var latestProducts = _productRepository.AsQueryable(cancellationToken: cancellationToken)
                                                .OrderByDescending(x => x.CreationDate)
                                                .Take(6)
                                                .ToList()
                                                .Select(x => _productHelper.MapProducts<ProductSiteDto>(x).Result)
                                                .ToList();

        return Task.FromResult((IEnumerable<ProductSiteDto>)latestProducts);
    }
}
