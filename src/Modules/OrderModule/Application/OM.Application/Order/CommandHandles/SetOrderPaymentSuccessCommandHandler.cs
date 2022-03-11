using _0_Framework.Application.Extensions;
using OM.Application.Contracts.Order.Commands;

namespace OM.Application.Order.CommandHandles;

public class SetOrderPaymentSuccessCommandHandler : IRequestHandler<SetOrderPaymentSuccessCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Order.Order> _orderRepository;
    private readonly IMapper _mapper;

    public SetOrderPaymentSuccessCommandHandler(IGenericRepository<Domain.Order.Order> orderRepository,
                                                    IMapper mapper)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(SetOrderPaymentSuccessCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        order.IsPaid = true;
        order.RefId = request.RefId;
        order.IssueTrackingNo = Generators.GenerateCode(8);

        await _orderRepository.UpdateAsync(order);

        return new Response<string>("");
    }
}