namespace AM.Application.Contracts.Services;

public interface IEmailSenderService
{
    bool SendEmail(string toId, string toName, string subject, string body);
}