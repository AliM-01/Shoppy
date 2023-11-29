using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.QueryHandles;

public record GetProductCategoriesSiteQuery() : IRequest<IEnumerable<SiteProductCategoryDto>>;

public class GetProductCategoriesSiteQueryHandler : IRequestHandler<GetProductCategoriesSiteQuery, IEnumerable<SiteProductCategoryDto>>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoriesSiteQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public Task<IEnumerable<SiteProductCategoryDto>> Handle(GetProductCategoriesSiteQuery request, CancellationToken cancellationToken)
    {
        var productCategories = _productCategoryRepository.AsQueryable()
                                                          .ToList()
                                                          .Select(c =>
                                                            _mapper.Map(c, new SiteProductCategoryDto()))
                                                          .ToList();

        return Task.FromResult((IEnumerable<SiteProductCategoryDto>)productCategories);
    }
}
