using _0_Framework.Application.Models.Paging;
using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.ArticleCategory.Queries.SiteCategory;

public record GetArticleCategoryWithArticlesSiteQuery(FilterArticleCategorySiteDto Filter) : IRequest<ArticleCategoryDetailsQueryModel>;

public class GetArticleCategoryWithArticlesSiteQueryHandler : IRequestHandler<GetArticleCategoryWithArticlesSiteQuery, ArticleCategoryDetailsQueryModel>
{
    private readonly IRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleCategoryWithArticlesSiteQueryHandler(
        IRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository,
        IRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ArticleCategoryDetailsQueryModel> Handle(GetArticleCategoryWithArticlesSiteQuery request, CancellationToken cancellationToken)
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
                _mapper.Map(article, new ArticleDetailsSiteDto()));

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Articles is null)
            throw new NoContentApiException();

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new ArticleCategoryDetailsQueryModel
        {
            ArticleCategory = _mapper.Map(articleCategoryData, new ArticleCategorySiteDto()),
            FilterData = filteredData
        };
    }
}
