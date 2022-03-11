using OM.Application.Contracts.Order.Commands;
using OM.Domain.Order;

namespace OM.Application.Order.CommandHandles;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Response<PlaceOrderResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Order.Order> _orderRepository;
    private readonly IGenericRepository<Domain.Order.OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public PlaceOrderCommandHandler(IGenericRepository<Domain.Order.Order> orderRepository,
        IGenericRepository<Domain.Order.OrderItem> orderItemRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<PlaceOrderResponseDto>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
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
                OrderId = order.Id,
                Order = order
            });

            await _orderItemRepository.InsertAsync(orderItem);
        }

        return new Response<PlaceOrderResponseDto>(new PlaceOrderResponseDto(order.Id));
    }
}