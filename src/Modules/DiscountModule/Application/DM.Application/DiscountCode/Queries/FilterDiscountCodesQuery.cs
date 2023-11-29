using _0_Framework.Application.Models.Paging;
using DM.Application.DiscountCode.DTOs;
using FluentValidation;
using MongoDB.Driver.Linq;

namespace DM.Application.DiscountCode.Queries;

public record FilterDiscountCodesQuery(FilterDiscountCodeDto Filter) : IRequest<FilterDiscountCodeDto>;

public class FilterDiscountCodesQueryValidator : AbstractValidator<FilterDiscountCodesQuery>
{
    public FilterDiscountCodesQueryValidator()
    {
        RuleFor(p => p.Filter.Phrase)
            .RequiredValidator("جستجو")
            .MaxLengthValidator("عنوان محصول", 100);
    }
}

public class FilterDiscountCodesQueryHandler : IRequestHandler<FilterDiscountCodesQuery, FilterDiscountCodeDto>
{
    private readonly IRepository<Domain.DiscountCode.DiscountCode> _discountCodeRepository;
    private readonly IMapper _mapper;

    public FilterDiscountCodesQueryHandler(IRepository<Domain.DiscountCode.DiscountCode> discountCodeRepository, IMapper mapper)
    {
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<FilterDiscountCodeDto> Handle(FilterDiscountCodesQuery request, CancellationToken cancellationToken)
    {
        var query = _discountCodeRepository.AsQueryable(cancellationToken: cancellationToken);

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.Phrase))
        {
            query = query.Where(s => s.Description.Contains(request.Filter.Phrase)
            || s.Code.Contains(request.Filter.Phrase));
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

        var pager = request.Filter.BuildPager((await query.CountAsync()));

        var allEntities =
            _discountCodeRepository
            .ApplyPagination(query, pager)
            .Select(discount =>
                _mapper.Map(discount, new DiscountCodeDto()))
            .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Discounts is null)
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NoContentApiException();

        return returnData;
    }
}