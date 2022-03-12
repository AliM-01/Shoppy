using _0_Framework.Application.ZarinPal;
using OM.Application.Contracts.Order.Commands;

namespace OM.Application.Order.CommandHandles;

public class InitializePaymentRequestCommandHandler : IRequestHandler<InitializePaymentRequestCommand, Response<InitializePaymentResponseDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Order.Order> _orderRepository;
    private readonly IZarinPalFactory _zarinPalFactory;

    public InitializePaymentRequestCommandHandler(IGenericRepository<Domain.Order.Order> orderRepository,
                                                    IZarinPalFactory zarinPalFactory)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _zarinPalFactory = Guard.Against.Null(zarinPalFactory, nameof(_zarinPalFactory));
    }

    #endregion

    public async Task<Response<InitializePaymentResponseDto>> Handle(InitializePaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Payment.OrderId);

        var paymentResponse = await _zarinPalFactory
                .CreatePaymentRequest(request.Payment.CallBackUrl, request.Payment.Amount, request.Payment.Email, order.Id);

        var redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + paymentResponse.Authority;

        return new Response<InitializePaymentResponseDto>(new InitializePaymentResponseDto(redirectUrl));
    }
}