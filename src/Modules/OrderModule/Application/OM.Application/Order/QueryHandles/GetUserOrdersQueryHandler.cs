using _0_Framework.Application.Models.Paging;
using AM.Domain.Account;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace OM.Application.Order.QueryHandles;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, ApiResult<GetUserOrdersDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly UserManager<Account> _userManager;
    private readonly IMapper _mapper;

    public GetUserOrdersQueryHandler(IRepository<Domain.Order.Order> orderRepository,
        UserManager<Account> userManager,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<GetUserOrdersDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        bool user = _userManager.Users.Any(x => x.Id == Guid.Parse(request.UserId));

        if (user is false)
            throw new NotFoundApiException("کاربری با این شناسه یافت نشد");

        var query = _orderRepository.AsQueryable().Where(x => x.UserId == request.UserId);

        #region filter

        if (!string.IsNullOrEmpty(request.Filter.IssueTrackingNo))
        {
            query = query.Where(x => x.IssueTrackingNo.Contains(request.Filter.IssueTrackingNo));
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

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync()), cancellationToken);

        var allEntities =
             _orderRepository
             .ApplyPagination(query, pager, cancellationToken)
             .Select(x =>
                _mapper.Map(x, new UserOrderDto()))
             .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (!returnData.Orders.Any())
            throw new NoContentApiException();

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NoContentApiException();

        return ApiResponse.Success<GetUserOrdersDto>(returnData);
    }
}
