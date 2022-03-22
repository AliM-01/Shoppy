using MongoDB.Driver;
using OM.Domain.Order;

namespace OM.Application.Order.QueryHandles;

public class GetInventoryItemsQueryHandler : IRequestHandler<GetInventoryItemsQuery, Response<List<OrderItemDto>>>
{
    #region Ctor

    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IRepository<Domain.Order.OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public GetInventoryItemsQueryHandler(IRepository<Domain.Order.Order> orderRepository,
        IRepository<Domain.Order.OrderItem> orderItemRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<List<OrderItemDto>>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new NotFoundApiException();

        if (!request.IsAdmin)
        {
            if (order.UserId != request.UserId)
                throw new NotFoundApiException();
        }

        var filter = Builders<OrderItem>.Filter.Where(x => x.OrderId == order.Id);

        var items = (await _orderItemRepository.GetManyByFilter(filter))
            .Select(x => _mapper.Map(x, new OrderItemDto()))
            .ToList();

        return new Response<List<OrderItemDto>>(items);
    }
}
