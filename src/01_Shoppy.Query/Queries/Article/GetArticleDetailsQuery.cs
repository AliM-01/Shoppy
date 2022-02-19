using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.Blog.Article;

public record GetArticleDetailsQuery(string Slug) : IRequest<Response<ArticleDetailsQueryModel>>;

public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Response<ArticleDetailsQueryModel>>
{
    #region Ctor

    private readonly IMongoHelper<BM.Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(
        IMongoHelper<BM.Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ArticleDetailsQueryModel>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var articles = await _articleHelper.AsQueryable()
            .Select(x => new
            {
                x.Slug,
                x.Id
            })
            .ToListAsyncSafe();

        var existsArticle = articles.FirstOrDefault(x => x.Slug == request.Slug);

        if (existsArticle is not null)
            throw new NotFoundApiException();

        var article = await _articleHelper.GetByIdAsync(existsArticle.Id);

        return new Response<ArticleDetailsQueryModel>(
            _mapper.Map(article, new ArticleDetailsQueryModel()));
    }
}
