using _0_Framework.Infrastructure;
using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.Article.Queries.Site;

public record GetRelatedArticlesSiteQuery(string CategoryId) : IRequest<IEnumerable<ArticleSiteDto>>;

public class GetRelatedArticlesSiteQueryHandler : IRequestHandler<GetRelatedArticlesSiteQuery, IEnumerable<ArticleSiteDto>>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetRelatedArticlesSiteQueryHandler(
        IRepository<Domain.Article.Article> articleRepository, IMapper mapper,
        IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    public async Task<IEnumerable<ArticleSiteDto>> Handle(GetRelatedArticlesSiteQuery request, CancellationToken cancellationToken)
    {
        var relatedArticles =
            (await _articleRepository
               .AsQueryable(cancellationToken: cancellationToken)
               .OrderByDescending(x => x.LastUpdateDate)
               .Where(x => x.CategoryId == request.CategoryId)
               .Take(2)
               .ToListAsyncSafe())
               .Select(article =>
                   _mapper.Map(article, new ArticleSiteDto()))
               .ToList();

        for (int i = 0; i < relatedArticles.Count; i++)
            relatedArticles[i].Category = (await _articleCategoryRepository.FindByIdAsync(relatedArticles[i].CategoryId)).Title;

        return relatedArticles;
    }
}
