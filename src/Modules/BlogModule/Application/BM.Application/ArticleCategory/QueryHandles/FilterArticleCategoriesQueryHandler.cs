using _0_Framework.Application.Models.Paging;
using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.ArticleCategory.QueryHandles;
public class FilterArticleCategoriesQueryHandler : IRequestHandler<FilterArticleCategoriesQuery, Response<FilterArticleCategoryDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.ArticleCategory.ArticleCategory> _articleCategoryHelper;
    private readonly IMapper _mapper;

    public FilterArticleCategoriesQueryHandler(IMongoHelper<Domain.ArticleCategory.ArticleCategory> articleCategoryHelper, IMapper mapper)
    {
        _articleCategoryHelper = Guard.Against.Null(articleCategoryHelper, nameof(_articleCategoryHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterArticleCategoryDto>> Handle(FilterArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _articleCategoryHelper.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
            query = query.Where(s => EF.Functions.Like(s.Title, $"%{request.Filter.Title}%"));

        switch (request.Filter.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        switch (request.Filter.SortIdOrder)
        {
            case PagingDataSortIdOrder.NotSelected:
                break;

            case PagingDataSortIdOrder.DES:
                query = query.OrderByDescending(x => x.Id);
                break;

            case PagingDataSortIdOrder.ASC:
                query = query.OrderBy(x => x.Id);
                break;
        }

        #endregion filter


        #region paging

        var pager = request.Filter.BuildPager(query.Count());

        var allEntities =
            _articleCategoryHelper
            .ApplyPagination(query, pager)
            .Select(article =>
                _mapper.Map(article, new ArticleCategoryDto()));

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ArticleCategories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterArticleCategoryDto>(returnData);
    }
}