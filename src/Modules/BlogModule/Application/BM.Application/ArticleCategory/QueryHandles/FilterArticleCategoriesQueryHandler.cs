using _0_Framework.Application.Models.Paging;
using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.ArticleCategory.QueryHandles;
public class FilterArticleCategoriesQueryHandler : IRequestHandler<FilterArticleCategoriesQuery, Response<FilterArticleCategoryDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public FilterArticleCategoriesQueryHandler(IGenericRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterArticleCategoryDto>> Handle(FilterArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _articleCategoryRepository.AsQueryable();

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
            _articleCategoryRepository
            .ApplyPagination(query, pager)
            .Select(article =>
                _mapper.Map(article, new ArticleCategoryDto()))
            .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ArticleCategories is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterArticleCategoryDto>(returnData);
    }
}