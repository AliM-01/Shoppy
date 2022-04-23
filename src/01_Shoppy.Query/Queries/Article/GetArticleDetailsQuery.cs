using _01_Shoppy.Query.Models.Blog.Article;

namespace _01_Shoppy.Query.Queries.Blog.Article;

public record GetArticleDetailsQuery(string Slug) : IRequest<ApiResult<ArticleDetailsQueryModel>>;

public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, ApiResult<ArticleDetailsQueryModel>>
{
    #region Ctor

    private readonly IRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailsQueryHandler(
        IRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper,
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    #endregion

    public async Task<ApiResult<ArticleDetailsQueryModel>> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(request.Slug))
            throw new NotFoundApiException();

        var filter = Builders<BM.Domain.Article.Article>.Filter.Eq(x => x.Slug, request.Slug);

        var article = await _articleRepository.FindOne(filter, cancellationToken);

        var meppedArticle = _mapper.Map(article, new ArticleDetailsQueryModel());

        meppedArticle.Category = (await _articleCategoryRepository.FindByIdAsync(article.CategoryId)).Title;

        return ApiResponse.Success<ArticleDetailsQueryModel>(meppedArticle);
    }
}
