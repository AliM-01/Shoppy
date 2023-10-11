using _0_Framework.Infrastructure;
using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.Article.Queries.Site;

public record GetLatestArticlesSiteQuery() : IRequest<List<ArticleSiteDto>>;

public class GetLatestArticlesSiteQueryHandler : IRequestHandler<GetLatestArticlesSiteQuery, IEnumerable<ArticleSiteDto>>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetLatestArticlesSiteQueryHandler(
        IRepository<Domain.Article.Article> articleRepository, IMapper mapper,
        IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    public async Task<IEnumerable<ArticleSiteDto>> Handle(GetLatestArticlesSiteQuery request, CancellationToken cancellationToken)
    {
        var latestArticles =
            (await _articleRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(2)
               .ToListAsyncSafe())
               .Select(article =>
                   _mapper.Map(article, new ArticleSiteDto()))
               .ToList();

        for (int i = 0; i < latestArticles.Count; i++)
            latestArticles[i].Category = (await _articleCategoryRepository.FindByIdAsync(latestArticles[i].CategoryId)).Title;

        return latestArticles;
    }
}
