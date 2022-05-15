namespace AM.Application.Contracts.Services;

public interface IEmailSenderService
{
    Task<bool> SendEmail(string email, string subject, string body);
}