using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Models.Blog.Article;
using _01_Shoppy.Query.Models.Blog.ArticleCategory;

namespace _01_Shoppy.Query.Queries.ArticleCategory;

public record GetArticleCategoryWithArticlesByQuery(FilterArticleCategoryDetailsModel Filter) : IRequest<ApiResult<ArticleCategoryDetailsQueryModel>>;

public class GetArticleCategoryWithArticlesByQueryHandler : IRequestHandler<GetArticleCategoryWithArticlesByQuery, ApiResult<ArticleCategoryDetailsQueryModel>>
{
    #region Ctor

    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryWithArticlesByQueryHandler(
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository,
        IRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<ArticleCategoryDetailsQueryModel>> Handle(GetArticleCategoryWithArticlesByQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        #region filter

        if (string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var filter = Builders<BM.Domain.ArticleCategory.ArticleCategory>.Filter.Eq(x => x.Slug, request.Filter.Slug);

        var articleCategoryData = await _articleCategoryRepository.FindOne(filter, cancellationToken);

        #endregion

        #region paging

        var articlesQuery = _articleRepository.AsQueryable(cancellationToken: cancellationToken)
             .Where(x => x.CategoryId == articleCategoryData.Id);

        var pager = request.Filter.BuildPager((await articlesQuery.CountAsync()), cancellationToken);

        var allEntities = _articleRepository
            .ApplyPagination(articlesQuery, pager, cancellationToken)
            .Select(article =>
                _mapper.Map(article, new ArticleDetailsQueryModel()));

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Articles is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        var returnData = new ArticleCategoryDetailsQueryModel
        {
            ArticleCategory = _mapper.Map(articleCategoryData, new ArticleCategoryQueryModel()),
            FilterData = filteredData
        };

        return ApiResponse.Success<ArticleCategoryDetailsQueryModel>(returnData);
    }
}
