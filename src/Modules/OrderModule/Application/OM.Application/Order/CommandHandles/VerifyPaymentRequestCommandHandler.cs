using _0_Framework.Application.ZarinPal;
using OM.Application.Contracts.Order.Commands;
using System.Globalization;

namespace OM.Application.Order.CommandHandles;

public class VerifyPaymentRequestCommandHandler : IRequestHandler<VerifyPaymentRequestCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.Order.Order> _orderRepository;
    private readonly IZarinPalFactory _zarinPalFactory;

    public VerifyPaymentRequestCommandHandler(IGenericRepository<Domain.Order.Order> orderRepository,
                                                    IZarinPalFactory zarinPalFactory)
    {
        _orderRepository = Guard.Against.Null(orderRepository, nameof(_orderRepository));
        _zarinPalFactory = Guard.Against.Null(zarinPalFactory, nameof(_zarinPalFactory));
    }

    #endregion

    public async Task<Response<string>> Handle(VerifyPaymentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Payment.OrderId);

        if (order is null)
            throw new NotFoundApiException();

        var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(request.Payment.Authority,
                    order.PaymentAmount.ToString(CultureInfo.InvariantCulture));

        return new Response<string>("");
    }
}