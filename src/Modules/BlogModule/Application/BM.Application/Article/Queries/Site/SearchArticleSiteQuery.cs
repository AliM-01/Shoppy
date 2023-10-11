using _0_Framework.Application.Models.Paging;
using BM.Application.ArticleCategory.Models.Site;

namespace BM.Application.Article.Queries.Site;

public record SearchArticleSiteQuery(SearchArticleSiteDto Search) : IRequest<SearchArticleSiteDto>;

public class SearchArticleSiteQueryHandler : IRequestHandler<SearchArticleSiteQuery, SearchArticleSiteDto>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public SearchArticleSiteQueryHandler(
        IRepository<Domain.Article.Article> articleRepository,
        IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<SearchArticleSiteDto> Handle(SearchArticleSiteQuery request, CancellationToken cancellationToken)
    {
        var query = _articleRepository.AsQueryable(cancellationToken: cancellationToken);

        #region filter selected categories slugs

        if (request.Search.SelectedCategories is not null && request.Search.SelectedCategories.Any())
        {
            var selectedCategoriesId = new List<string>();

            foreach (string categorySlug in request.Search.SelectedCategories)
                if (await _articleCategoryRepository.ExistsAsync(x => x.Slug == categorySlug.Trim()))
                {
                    var filter = Builders<Domain.ArticleCategory.ArticleCategory>.Filter.Eq(x => x.Slug, categorySlug.Trim());
                    var category = await _articleCategoryRepository.FindOne(filter);
                    selectedCategoriesId.Add(category.Id);
                }

            query = query.Where(x => selectedCategoriesId.Contains(x.CategoryId));
        }

        #endregion

        #region filter phrase

        if (!string.IsNullOrEmpty(request.Search.Phrase))
        {
            var titleIds = await _articleRepository.FullTextSearch(x => x.Title,
                request.Search.Phrase, cancellationToken);

            query = query.Where(x => titleIds.Contains(x.Id));
        }

        #endregion

        #region paging

        var pager = request.Search.BuildPager(await query.CountAsync(), cancellationToken);

        var allEntities =
             _articleRepository
             .ApplyPagination(query, pager, cancellationToken)
             .Select(article =>
                   _mapper.Map(article, new ArticleSiteDto()))
             .ToList();

        #endregion paging

        var returnData = request.Search.SetData(allEntities).SetPaging(pager);

        if (returnData.Articles is null)
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return returnData;

    }

}
