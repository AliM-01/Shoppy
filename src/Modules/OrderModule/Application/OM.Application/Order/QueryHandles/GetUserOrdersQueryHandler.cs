using AM.Domain.Account;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace OM.Application.Order.QueryHandles;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, Response<List<OrderDto>>>
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

    public async Task<Response<List<OrderDto>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.Any(x => x.Id == Guid.Parse(request.UserId));

        if (user is false)
            throw new NotFoundApiException("کاربری با این شناسه یافت نشد");

        var filter = Builders<Domain.Order.Order>.Filter.Eq(x => x.UserId, request.UserId);

        var orders = await _orderRepository.GetManyByFilter(filter, cancellationToken);

        if (!orders.Any())
            throw new NoContentApiException("کاربر سفارشی ندارد");

        return new Response<List<OrderDto>>(orders
                                                  .Select(x =>
                                                        _mapper.Map(x, new OrderDto()))
                                                  .ToList());
    }
}
