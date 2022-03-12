namespace _0_Framework.Application.ZarinPal;

public interface IZarinPalFactory
{
    Task<PaymentResponse> CreatePaymentRequest(string callBackUrl, string amount, string email, long orderId);

    Task<VerificationResponse> CreateVerificationRequest(string authority, string price);
}
