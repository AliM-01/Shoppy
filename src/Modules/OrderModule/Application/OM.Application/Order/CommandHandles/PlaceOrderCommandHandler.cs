using OM.Application.Contracts.Order.Commands;
using OM.Domain.Order;

namespace OM.Application.Order.CommandHandles;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, ApiResult<PlaceOrderResponseDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IRepository<Domain.Order.OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public PlaceOrderCommandHandler(IRepository<Domain.Order.Order> orderRepository,
        IRepository<Domain.Order.OrderItem> orderItemRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<PlaceOrderResponseDto>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map(request, new Domain.Order.Order
        {
            IssueTrackingNo = "0000-0000"
        });

        _mapper.Map(request, order);

        await _orderRepository.InsertAsync(order);

        foreach (var cartItem in request.Cart.Items)
        {
            var orderItem = _mapper.Map(cartItem, new OrderItem
            {
                OrderId = order.Id.ToString()
            });

            await _orderItemRepository.InsertAsync(orderItem);
        }

        return ApiResponse.Success<PlaceOrderResponseDto>(new PlaceOrderResponseDto(order.Id));
    }
}