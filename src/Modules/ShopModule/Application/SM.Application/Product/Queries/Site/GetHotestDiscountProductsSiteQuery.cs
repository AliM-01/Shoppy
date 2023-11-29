using SM.Application.Product.DTOs.Site;
using SM.Application.Services;

namespace SM.Application.Product.Queries.Site;

public record GetHotestDiscountProductsSiteQuery() : IRequest<IEnumerable<ProductSiteDto>>;

public class GetHotestDiscountProductsSiteQueryHandler : IRequestHandler<GetHotestDiscountProductsSiteQuery, IEnumerable<ProductSiteDto>>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IProductDiscountAclService _productDiscountAcl;
    private readonly IMapper _mapper;
    private readonly IProductHelper _productHelper;

    public GetHotestDiscountProductsSiteQueryHandler(IRepository<Domain.Product.Product> productRepository,
        IProductDiscountAclService productDiscountAcl,
        IMapper mapper, IProductHelper productHelper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productDiscountAcl = Guard.Against.Null(productDiscountAcl, nameof(_productDiscountAcl));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<ProductSiteDto>> Handle(GetHotestDiscountProductsSiteQuery request, CancellationToken cancellationToken)
    {
        var hotDiscountRateIds = _productDiscountAcl.GetHotestDiscountProductIds();

        var products = _productRepository.AsQueryable()
                                         .Where(x => hotDiscountRateIds.Contains(x.Id))
                                         .OrderByDescending(x => x.LastUpdateDate)
                                         .Take(6)
                                         .ToList()
                                         .Select(x => _productHelper.MapProducts<ProductSiteDto>(x, true).Result);

        return Task.FromResult(products);
    }
}
