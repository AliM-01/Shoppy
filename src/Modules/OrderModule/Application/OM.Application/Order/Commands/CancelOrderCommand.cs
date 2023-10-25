namespace OM.Application.Order.Commands;

public record CancelOrderCommand(string OrderId, string UserId, bool IsAdmin) : IRequest<ApiResult>;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ApiResult>
{
    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IRepository<Domain.Order.OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public CancelOrderCommandHandler(IRepository<Domain.Order.Order> orderRepository,
        IRepository<Domain.Order.OrderItem> orderItemRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _orderItemRepository = Guard.Against.Null(orderItemRepository, nameof(_orderItemRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.OrderId);

        NotFoundApiException.ThrowIfNull(order);

        if (!request.IsAdmin)
        {
            if (order.UserId != request.UserId)
                throw new NotFoundApiException();
        }

        if (order.IsPaid)
            throw new ApiException("سفارش قبلا پرداخت شده و قابل لغو شدن نمی باشد");

        order.IsCanceled = true;
        order.IssueTrackingNo = "0000-0000";

        await _orderRepository.UpdateAsync(order);

        return ApiResponse.Success();
    }
}