using _0_Framework.Application.Models.Paging;
using Microsoft.EntityFrameworkCore;
using OM.Application.Contracts.Order.Enums;

namespace OM.Application.Order.QueryHandles;

public class FilterOrdersQueryHandler : IRequestHandler<FilterOrdersQuery, Response<FilterOrderDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Order.Order> _orderRepository;
    private readonly IMapper _mapper;

    public FilterOrdersQueryHandler(IGenericRepository<Domain.Order.Order> orderRepository, IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterOrderDto>> Handle(FilterOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _orderRepository.AsQueryable();

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.UserNames))
            query = query.Where(s => EF.Functions.Like(s.UserId, $"%{request.Filter.UserNames}%") ||
             EF.Functions.Like(s.UserId, $"%{request.Filter.UserNames}%"));

        switch (request.Filter.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        switch (request.Filter.PaymentState)
        {
            case FilterOrderPaymentStatus.All:
                break;

            case FilterOrderPaymentStatus.IsPaid:
                query = query.OrderBy(x => x.IsPaid);
                break;

            case FilterOrderPaymentStatus.IsCanceled:
                query = query.OrderBy(x => x.IsCanceled);
                break;

            case FilterOrderPaymentStatus.PaymenyPending:
                query = query.OrderBy(x => (!x.IsCanceled && !x.IsPaid));
                break;
        }

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync()));

        var allEntities =
             _orderRepository
             .ApplyPagination(query, pager)
             .Select(Order =>
                _mapper.Map(Order, new OrderDto()))
             .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.Orders is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterOrderDto>(returnData);
    }
}