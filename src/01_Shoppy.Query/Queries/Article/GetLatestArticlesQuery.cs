using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.Article;

public record GetLatestArticlesQuery() : IRequest<Response<IEnumerable<ArticleQueryModel>>>;

public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, Response<IEnumerable<ArticleQueryModel>>>
{
    #region Ctor

    private readonly IMongoHelper<BM.Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public GetLatestArticlesQueryHandler(
        IMongoHelper<BM.Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleQueryModel>>> Handle(GetLatestArticlesQuery request, CancellationToken cancellationToken)
    {
        var latestArticles = await _articleHelper
               .AsQueryable()
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(article =>
                   _mapper.Map(article, new ArticleQueryModel()))
               .ToListAsyncSafe();

        return new Response<IEnumerable<ArticleQueryModel>>(latestArticles);
    }
}
