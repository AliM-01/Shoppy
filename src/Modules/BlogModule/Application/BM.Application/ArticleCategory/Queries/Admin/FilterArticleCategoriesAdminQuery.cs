using _0_Framework.Application.Models.Paging;
using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.ArticleCategory.Queries.Admin;

public record FilterArticleCategoriesAdminQuery(FilterArticleCategoryAdminDto Filter) : IRequest<FilterArticleCategoryAdminDto>;

public class FilterArticleCategoriesAdminQueryValidator : AbstractValidator<FilterArticleCategoriesAdminQuery>
{
    public FilterArticleCategoriesAdminQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);
    }
}

public class FilterArticleCategoriesAdminQueryHandler : IRequestHandler<FilterArticleCategoriesAdminQuery, FilterArticleCategoryAdminDto>
{
    private readonly IRepository<Domain.ArticleCategory.ArticleCategory> _articleCategoryRepository;
    private readonly IMapper _mapper;

    public FilterArticleCategoriesAdminQueryHandler(IRepository<Domain.ArticleCategory.ArticleCategory> articleCategoryRepository, IMapper mapper)
    {
        _articleCategoryRepository = Guard.Against.Null(articleCategoryRepository, nameof(_articleCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<FilterArticleCategoryAdminDto> Handle(FilterArticleCategoriesAdminQuery request, CancellationToken cancellationToken)
    {
        var query = _articleCategoryRepository.AsQueryable(cancellationToken: cancellationToken);

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
            query = query.Where(s => s.Title.Contains(request.Filter.Title));

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

        var pager = request.Filter.BuildPager(await query.CountAsync(), cancellationToken);

        var allEntities =
            _articleCategoryRepository
            .ApplyPagination(query, pager, cancellationToken)
            .Select(article =>
                _mapper.Map(article, new ArticleCategoryAdminDto()))
            .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ArticleCategories is null)
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NoContentApiException();

        return returnData;
    }
}