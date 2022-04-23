using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Models.Blog.Article;

namespace _01_Shoppy.Query.Queries.Article;

public record GetLatestArticlesQuery() : IRequest<ApiResult<List<ArticleQueryModel>>>;

public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, ApiResult<List<ArticleQueryModel>>>
{
    #region Ctor

    private readonly IRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetLatestArticlesQueryHandler(
        IRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper,
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    #endregion

    public async Task<ApiResult<List<ArticleQueryModel>>> Handle(GetLatestArticlesQuery request, CancellationToken cancellationToken)
    {
        var latestArticles =
            (await _articleRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(2)
               .ToListAsyncSafe())
               .Select(article =>
                   _mapper.Map(article, new ArticleQueryModel()))
               .ToList();

        for (int i = 0; i < latestArticles.Count; i++)
        {
            latestArticles[i].Category = (await _articleCategoryRepository.FindByIdAsync(latestArticles[i].CategoryId)).Title;
        }

        return ApiResponse.Success<List<ArticleQueryModel>>(latestArticles);
    }
}
