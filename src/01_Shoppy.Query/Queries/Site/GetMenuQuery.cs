using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using _01_Shoppy.Query.Models.ProductCategory;
using _01_Shoppy.Query.Models.Site;

namespace _01_Shoppy.Query.Queries.Site;

public record GetMenuQuery() : IRequest<Response<MenuDto>>;

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, Response<MenuDto>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetMenuQueryHandler(IGenericRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository,
                               IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository,
                               IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion


    public async Task<Response<MenuDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var productCategories = (await _productCategoryRepository.AsQueryable().ToListAsyncSafe())
             .Select(productCategory => _mapper.Map(productCategory, new ProductCategoryQueryModel()))
             .ToList();

        var articleCategories = (await _articleCategoryRepository.AsQueryable().ToListAsyncSafe())
             .Select(articleCategory => _mapper.Map(articleCategory, new ArticleCategoryQueryModel()))
             .ToList();

        return new Response<MenuDto>(new MenuDto(productCategories, articleCategories));
    }
}
