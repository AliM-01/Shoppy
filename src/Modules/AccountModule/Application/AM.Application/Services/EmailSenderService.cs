using AM.Application.Contracts.Services;

namespace AM.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    public bool SendEmail(string toId, string toName, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
