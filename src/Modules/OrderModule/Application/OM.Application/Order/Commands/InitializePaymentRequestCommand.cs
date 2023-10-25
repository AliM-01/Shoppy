using _0_Framework.Application.ZarinPal;
using System.Globalization;

namespace OM.Application.Order.Commands;

public record InitializePaymentRequestCommand(InitializePaymentRequestDto Payment) : IRequest<InitializePaymentResponseDto>;

public class InitializePaymentRequestCommandHandler : IRequestHandler<InitializePaymentRequestCommand, InitializePaymentResponseDto>
{
    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IZarinPalFactory _zarinPalFactory;

    public InitializePaymentRequestCommandHandler(IRepository<Domain.Order.Order> orderRepository,
                                                    IZarinPalFactory zarinPalFactory)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _zarinPalFactory = Guard.Against.Null(zarinPalFactory, nameof(_zarinPalFactory));
    }

    public async Task<InitializePaymentResponseDto> Handle(InitializePaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.Payment.OrderId);

        NotFoundApiException.ThrowIfNull(order);

        var paymentResponse = await _zarinPalFactory
                .CreatePaymentRequest(request.Payment.CallBackUrl, request.Payment.Amount.ToString(CultureInfo.InvariantCulture), request.Payment.Email, order.Id);

        string redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + paymentResponse.Authority;

        return new InitializePaymentResponseDto(redirectUrl);
    }
}