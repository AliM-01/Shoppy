using AM.Application.Contracts.Services;
using AM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Mail;

namespace AM.Application.Services;

public class EmailSenderService : IEmailSenderService
{
    #region Ctor

    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSenderService> _logger;
    private const int MAX_RETRIES = 3;
    private AsyncRetryPolicy _retryPolicy;

    public EmailSenderService(IOptions<EmailSettings> options, ILogger<EmailSenderService> logger)
    {
        _emailSettings = options.Value;
        _logger = logger;
        _retryPolicy = Policy.Handle<Exception>()
                                .WaitAndRetryAsync(
                                           retryCount: MAX_RETRIES,
                                           sleepDurationProvider: times => TimeSpan.FromMilliseconds(times * 250), // 250ms == 0.25s
                                           onRetry: (exception, sleepDuration, attemptNumber, context) =>
                                           {
                                               logger.LogError("Email Sending Error. Retrying in {0}. {1}/{2}", sleepDuration, attemptNumber, MAX_RETRIES);
                                           });
    }

    #endregion

    // Ports :
    // Not-Encrypted 25
    // Secure Tls 587
    // Secure SSL 465
    public async Task<bool> SendEmail(string email, string subject, string body)
    {
        return await _retryPolicy.ExecuteAsync<bool>(async () =>
        {
            using (SmtpClient client = new(_emailSettings.Host, _emailSettings.Port))
            {
                client.EnableSsl = _emailSettings.UseSSL;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.EmailId, _emailSettings.Password);

                MailMessage message = new();
                message.To.Add(email);
                message.From = new MailAddress(_emailSettings.EmailId, _emailSettings.Name);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                await client.SendMailAsync(message);
                return true;
            }
        });
    }
}
