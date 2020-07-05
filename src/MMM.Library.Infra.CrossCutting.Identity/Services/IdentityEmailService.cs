using KissLog;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MMM.Library.Infra.CrossCutting.Email.SendGridProvider;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Identity.Services
{
    public class IdentityEmailService : IEmailSender
    {
        public SendGripApiOptions Options { get; } //set only via Secret Manager
        private readonly ILogger _logger;

        public IdentityEmailService(IOptions<SendGripApiOptions> optionsAccessor, ILogger logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(Options.SendGridKey, subject, message, email);
                _logger.Info("Email sent to: " + email);
            }
            catch (System.Exception)
            {
                _logger.Error("Error sending email to: " + email);
            }

            return Task.CompletedTask;
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.EmailSender, Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
            };
            msg.AddTo(new EmailAddress(email));

            // format message text
            msg.PlainTextContent = message;
            msg.HtmlContent = message;


            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
