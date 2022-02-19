using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;
using BM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Article;

public record GetLatestArticlesQuery() : IRequest<Response<IEnumerable<ArticleQueryModel>>>;

public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, Response<IEnumerable<ArticleQueryModel>>>
{
    #region Ctor

    private readonly BlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public GetLatestArticlesQueryHandler(BlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ArticleQueryModel>>> Handle(GetLatestArticlesQuery request, CancellationToken cancellationToken)
    {
        var latestArticles = await _blogContext.Articles
               .AsQueryable()
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(Article =>
                   _mapper.Map(Article, new ArticleQueryModel()))
               .ToListAsyncSafe();

        return new Response<IEnumerable<ArticleQueryModel>>(latestArticles);
    }
}
