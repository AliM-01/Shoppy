using _0_Framework.Application.ZarinPal;
using OM.Application.Contracts.Order.Commands;
using System.Globalization;

namespace OM.Application.Order.CommandHandles;

public class InitializePaymentRequestCommandHandler : IRequestHandler<InitializePaymentRequestCommand, ApiResult<InitializePaymentResponseDto>>
{
    #region Ctor

    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IZarinPalFactory _zarinPalFactory;

    public InitializePaymentRequestCommandHandler(IRepository<Domain.Order.Order> orderRepository,
                                                    IZarinPalFactory zarinPalFactory)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _zarinPalFactory = Guard.Against.Null(zarinPalFactory, nameof(_zarinPalFactory));
    }

    #endregion

    public async Task<ApiResult<InitializePaymentResponseDto>> Handle(InitializePaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.Payment.OrderId);

        if (order is null)
            throw new NotFoundApiException();

        var paymentResponse = await _zarinPalFactory
                .CreatePaymentRequest(request.Payment.CallBackUrl, request.Payment.Amount.ToString(CultureInfo.InvariantCulture), request.Payment.Email, order.Id);

        var redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + paymentResponse.Authority;

        return ApiResponse.Success<InitializePaymentResponseDto>(new InitializePaymentResponseDto(redirectUrl));
    }
}