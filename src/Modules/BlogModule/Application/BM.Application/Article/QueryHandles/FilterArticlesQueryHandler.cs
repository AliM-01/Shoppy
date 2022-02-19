using _0_Framework.Application.Models.Paging;
using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.Article.QueryHandles;
public class FilterArticlesQueryHandler : IRequestHandler<FilterArticlesQuery, Response<FilterArticleDto>>
{
    #region Ctor

    private readonly IMongoHelper<Domain.Article.Article> _articleHelper;
    private readonly IMapper _mapper;

    public FilterArticlesQueryHandler(IMongoHelper<Domain.Article.Article> articleHelper, IMapper mapper)
    {
        _articleHelper = Guard.Against.Null(articleHelper, nameof(_articleHelper));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterArticleDto>> Handle(FilterArticlesQuery request, CancellationToken cancellationToken)
    {
        var query = _articleHelper.AsQueryable();

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
            _articleHelper
            .ApplyPagination(query, pager)
            .Select(article =>
                _mapper.Map(article, new ArticleDto()));

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Articles is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterArticleDto>(returnData);
    }
}