using _0_Framework.Application.Extensions;
using OM.Application.Contracts.Order.Commands;
using OM.Domain.Order;

namespace OM.Application.Order.CommandHandles;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Response<long>>
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

    public async Task<Response<long>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Order.Order
        {
            UserId = request.UserId,
            PaymentAmount = request.Cart.PayAmount,
            TotalAmount = request.Cart.TotalAmount,
            DiscountAmount = request.Cart.DiscountAmount,
            PaymentMethod = request.Cart.PaymentMethod,
            IssueTrackingNo = Generators.GenerateCode(8)
        };

        await _orderRepository.InsertAsync(order);

        foreach (var cartItem in request.Cart.Items)
        {
            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                Count = cartItem.Count,
                UnitPrice = cartItem.UnitPrice,
                DiscountRate = cartItem.DiscountRate,
                OrderId = order.Id
            };

            await _orderItemRepository.InsertAsync(orderItem);
        }


        return new Response<long>(1);
    }
}