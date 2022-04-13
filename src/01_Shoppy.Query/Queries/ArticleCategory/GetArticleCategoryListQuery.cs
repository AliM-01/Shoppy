using _01_Shoppy.Query.Models.Blog.ArticleCategory;

namespace _01_Shoppy.Query.Queries.ArticleCategory;

public record GetArticleCategoryListQuery() : IRequest<ApiResult<List<ArticleCategoryQueryModel>>>;

public class GetArticleCategoryListQueryHandler : IRequestHandler<GetArticleCategoryListQuery, ApiResult<List<ArticleCategoryQueryModel>>>
{
    #region Ctor

    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryListQueryHandler(
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public Task<ApiResult<List<ArticleCategoryQueryModel>>> Handle(GetArticleCategoryListQuery request, CancellationToken cancellationToken)
    {
        var articleCategories =
            _articleCategoryRepository
            .AsQueryable(cancellationToken: cancellationToken)
            .ToList()
            .Select(articleCategory => _mapper.Map(articleCategory, new ArticleCategoryQueryModel()))
            .ToList();

        return Task.FromResult(ApiResponse.Success<List<ArticleCategoryQueryModel>>(articleCategories));
    }
}
