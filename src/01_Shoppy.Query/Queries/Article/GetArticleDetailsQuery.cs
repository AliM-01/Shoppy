using _0_Framework.Application.Exceptions;
using _01_Shoppy.Query.Models.Blog.Article;
using AutoMapper;
using BM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Blog.Article;

public record GetArticleDetailsQuery(string Slug) : IRequest<Response<ArticleDetailsQueryModel>>;

public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Response<ArticleDetailsQueryModel>>
{
    #region Ctor

    private readonly BlogDbContext _blogContext;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(
        BlogDbContext blogContext, IMapper mapper)
    {
        _blogContext = Guard.Against.Null(blogContext, nameof(_blogContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ArticleDetailsQueryModel>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var articles = await _blogContext.Articles.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        var existsArticle = articles.FirstOrDefault(x => x.Slug == request.Slug);

        if (existsArticle is not null)
            throw new NotFoundApiException();

        var article = _blogContext.Articles
                .Where(x => x.Id == existsArticle.Id)
                .AsQueryable()
                .Select(c =>
                   _mapper.Map(c, new ArticleDetailsQueryModel()))
                .FirstOrDefault();

        return new Response<ArticleDetailsQueryModel>(article);
    }
}
