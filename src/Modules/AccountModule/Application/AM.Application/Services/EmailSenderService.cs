using AM.Application.Contracts.Services;
using AM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace AM.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    #region Ctor

    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(IOptions<EmailSettings> options, ILogger<EmailSenderService> logger)
    {
        _emailSettings = options.Value;
        _logger = logger;
    }

    #endregion

    // Ports :
    // Not-Encrypted 25
    // Secure Tls 587
    // Secure SSL 465
    public bool SendEmail(string toId, string toName, string subject, string body)
    {
        try
        {
            using (SmtpClient client = new(_emailSettings.Host, _emailSettings.Port))
            {
                client.EnableSsl = _emailSettings.UseSSL;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.EmailId, _emailSettings.Password);

                MailMessage message = new();
                message.To.Add(toId);
                message.From = new MailAddress(_emailSettings.EmailId, _emailSettings.Name);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                client.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError("InnerException is: {0}", ex.InnerException);
            return false;
        }
    }
}
