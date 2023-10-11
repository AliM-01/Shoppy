using _0_Framework.Application.Models.Paging;
using BM.Application.Article.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BM.Application.Article.Queries.Admin;

public record FilterArticlesAdminQuery(FilterArticleDto Filter) : IRequest<FilterArticleDto>;

public class FilterArticlesAdminQueryValidator : AbstractValidator<FilterArticlesAdminQuery>
{
    public FilterArticlesAdminQueryValidator()
    {
        RuleFor(p => p.Filter.Title)
            .MaxLengthValidator("عنوان", 100);
    }
}

public class FilterArticlesAdminQueryHandler : IRequestHandler<FilterArticlesAdminQuery, FilterArticleDto>
{
    private readonly IRepository<Domain.Article.Article> _articleRepository;
    private readonly IMapper _mapper;

    public FilterArticlesAdminQueryHandler(IRepository<Domain.Article.Article> articleRepository, IMapper mapper)
    {
        _articleRepository = Guard.Against.Null(articleRepository, nameof(_articleRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<FilterArticleDto> Handle(FilterArticlesAdminQuery request, CancellationToken cancellationToken)
    {
        var query = _articleRepository.AsQueryable(cancellationToken: cancellationToken);

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Title))
        {
            var ids = await _articleRepository.FullTextSearch(x => x.Title,
                request.Filter.Title, cancellationToken);

            query.Where(x => ids.Contains(x.Id));

        }

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
            _articleRepository
            .ApplyPagination(query, pager, cancellationToken)
            .Select(article =>
                _mapper.Map(article, new ArticleDto()))
            .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Articles is null)
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NoContentApiException();

        return returnData;
    }
}