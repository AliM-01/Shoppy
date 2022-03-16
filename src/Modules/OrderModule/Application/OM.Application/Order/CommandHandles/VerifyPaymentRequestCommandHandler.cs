using _0_Framework.Application.Extensions;
using _0_Framework.Application.ZarinPal;
using OM.Application.Contracts.Order.Commands;
using System.Globalization;

namespace OM.Application.Order.CommandHandles;

public class VerifyPaymentRequestCommandHandler : IRequestHandler<VerifyPaymentRequestCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.Order.Order> _orderRepository;
    private readonly IZarinPalFactory _zarinPalFactory;

    public VerifyPaymentRequestCommandHandler(IRepository<Domain.Order.Order> orderRepository,
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

        var verificationResponse = await _zarinPalFactory
            .CreateVerificationRequest(request.Payment.Authority,
                    order.PaymentAmount.ToString(CultureInfo.InvariantCulture));

        if (!(verificationResponse.Status >= 100))
            throw new ApiException("پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");

        order.IsPaid = true;
        order.RefId = verificationResponse.RefID;
        order.IssueTrackingNo = Generator.Code(8);

        await _orderRepository.UpdateAsync(order);

        return new Response<string>("پرداخت با موفقیت انجام شد");
    }
}