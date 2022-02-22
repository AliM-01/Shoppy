using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.Article;

public record GetLatestArticlesQuery() : IRequest<Response<IEnumerable<ArticleQueryModel>>>;

public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, Response<IEnumerable<ArticleQueryModel>>>
{
    #region Ctor

    private readonly IGenericRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetLatestArticlesQueryHandler(
        IGenericRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleQueryModel>>> Handle(GetLatestArticlesQuery request, CancellationToken cancellationToken)
    {
        var latestArticles =
            await _articleRepository
               .AsQueryable()
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .ToListAsyncSafe();

        return new Response<IEnumerable<ArticleQueryModel>>(
            latestArticles
            .Select(article =>
                   _mapper.Map(article, new ArticleQueryModel())));
    }
}
