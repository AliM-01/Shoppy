using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Models.Blog.Article;

namespace _01_Shoppy.Query.Queries.Article;

public record ArticleSearchQuery(SearchArticleQueryModel Search) : IRequest<Response<SearchArticleQueryModel>>;

public class ArticleSearchQueryHandler : IRequestHandler<ArticleSearchQuery, Response<SearchArticleQueryModel>>
{
    #region Ctor

    private readonly IGenericRepository<BM.Domain.Article.Article> _articleRepository;
    private readonly IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public ArticleSearchQueryHandler(
        IGenericRepository<BM.Domain.Article.Article> articleRepository, IMapper mapper,
        IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryRepository)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
    }

    #endregion

    public async Task<Response<SearchArticleQueryModel>> Handle(ArticleSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _articleRepository.AsQueryable();

        #region filter selected categories slugs

        if (request.Search.SelectedCategories is not null && request.Search.SelectedCategories.Any())
        {
            var selectedCategoriesId = new List<string>();

            foreach (var categorySlug in request.Search.SelectedCategories)
            {
                if (await _articleCategoryRepository.ExistsAsync(x => x.Slug.Trim() == categorySlug.Trim()))
                {
                    var filter = Builders<BM.Domain.ArticleCategory.ArticleCategory>.Filter
                        .Eq(x => x.Slug.Trim(), categorySlug.Trim());

                    var category = await _articleCategoryRepository.GetByFilter(filter);
                    selectedCategoriesId.Add(category.Id);
                }
            }

            query = query.Where(x => selectedCategoriesId.Contains(x.CategoryId));
        }

        #endregion

        #region filter

        #region filter phrase

        if (!string.IsNullOrEmpty(request.Search.Phrase))
        {
            query = query.Where(s => s.Title.Contains(request.Search.Phrase)
            || s.Summary.Contains(request.Search.Phrase) || s.MetaKeywords.Contains(request.Search.Phrase));
        }

        #endregion

        #region filter sort date order

        switch (request.Search.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        #endregion

        #endregion filter

        #region paging

        var pager = request.Search.BuildPager(query.Count());

        var allEntities =
             _articleRepository
             .ApplyPagination(query, pager)
             .Select(article =>
                   _mapper.Map(article, new ArticleQueryModel()))
             .ToList();

        #endregion paging

        var returnData = request.Search.SetData(allEntities).SetPaging(pager);

        if (returnData.Articles is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<SearchArticleQueryModel>(returnData);
    }
}

