using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.Blog.Article;
using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.ArticleCategory;

public record GetArticleCategoryWithArticlesByQuery(FilterArticleCategoryDetailsModel Filter) : IRequest<Response<ArticleCategoryDetailsQueryModel>>;

public class GetArticleCategoryWithArticlesByQueryHandler : IRequestHandler<GetArticleCategoryWithArticlesByQuery, Response<ArticleCategoryDetailsQueryModel>>
{
    #region Ctor

    private readonly IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> _articleCategoryHelper;
    private readonly IGenericRepository<BM.Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public GetArticleCategoryWithArticlesByQueryHandler(
        IGenericRepository<BM.Domain.ArticleCategory.ArticleCategory> articleCategoryHelper,
        IGenericRepository<BM.Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleCategoryHelper = Guard.Against.Null(articleCategoryHelper, nameof(_articleCategoryHelper));
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ArticleCategoryDetailsQueryModel>> Handle(GetArticleCategoryWithArticlesByQuery request, CancellationToken cancellationToken)
    {
        #region filter

        if (string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var filter = Builders<BM.Domain.ArticleCategory.ArticleCategory>.Filter.Eq(x => x.Slug, request.Filter.Slug);

        var articleCategoryData = await _articleCategoryHelper.GetByFilter(filter);

        #endregion

        #region paging

        var articlesQuery = _articleHelper.AsQueryable()
             .Where(x => x.CategoryId == articleCategoryData.Id);

        var pager = request.Filter.BuildPager(articlesQuery.Count());

        var allEntities =
            _articleHelper
            .ApplyPagination(articlesQuery, pager)
            .Select(article =>
                _mapper.Map(article, new ArticleDetailsQueryModel()));

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Articles is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        var returnData = new ArticleCategoryDetailsQueryModel();

        returnData.ArticleCategory = _mapper.Map(articleCategoryData, new ArticleCategoryQueryModel());
        returnData.FilterData = filteredData;

        return new Response<ArticleCategoryDetailsQueryModel>(returnData);
    }
}
