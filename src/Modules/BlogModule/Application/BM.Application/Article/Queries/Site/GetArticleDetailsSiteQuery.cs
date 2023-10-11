using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.Article.Queries.Site;

public record GetArticleDetailsSiteQuery(string Slug) : IRequest<ArticleDetailsSiteDto>;

public class GetArticleDetailsSiteQueryHandler : IRequestHandler<GetArticleDetailsSiteQuery, ArticleDetailsSiteDto>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailsSiteQueryHandler(
        IRepository<Domain.Article.Article> articleRepository, IMapper mapper,
        IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    public async Task<ArticleDetailsSiteDto> Handle(GetArticleDetailsSiteQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var filter = Builders<Domain.Article.Article>.Filter.Eq(x => x.Slug, request.Slug);

        var article = await _articleRepository.FindOne(filter, cancellationToken);

        var meppedArticle = _mapper.Map(article, new ArticleDetailsSiteDto());

        meppedArticle.Category = (await _articleCategoryRepository.FindByIdAsync(article.CategoryId)).Title;

        return meppedArticle;
    }
}
