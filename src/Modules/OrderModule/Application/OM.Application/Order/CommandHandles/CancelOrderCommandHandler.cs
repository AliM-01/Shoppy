using OM.Application.Contracts.Order.Commands;

namespace OM.Application.Order.CommandHandles;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Response<string>>
{
    #region Ctor

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

    #endregion

    public async Task<Response<string>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
            throw new NotFoundApiException();

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

        return new Response<string>(ApplicationErrorMessage.OperationSuccedded);
    }
}