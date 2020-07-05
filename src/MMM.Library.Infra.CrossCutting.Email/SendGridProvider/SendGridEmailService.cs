using KissLog;
using Microsoft.Extensions.Options;
using MMM.Library.Domain.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Email.SendGridProvider
{
    public class SendGridEmailService : IEmailService
    {
        public SendGripApiOptions SendGripOpts { get; } //set only via Secret Manager
        private readonly ILogger _logger;

        public SendGridEmailService(IOptions<SendGripApiOptions> optionsAccessor, ILogger logger)
        {
            SendGripOpts = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task<string> SendEmailAsync(IEnumerable<string> mailToList, string mailFrom,
            string subject, string body, bool isBodyHtml)
        {
            try
            {
                await Execute(SendGripOpts.SendGridKey, subject, body, mailToList);
                _logger.Info("Email sent to: " + "");
            }
            catch (System.Exception)
            {
                _logger.Error("Error sending email to: " + "");
            }

            return string.Concat("Email sent to: ") + string.Join(" ", mailToList);
        }

        public async Task Execute(string apiKey, string subject, string body, IEnumerable<string> emailToList)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(SendGripOpts.EmailSender, SendGripOpts.SendGridUser),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body,
            };
            //message.To.AddRange(emailMessage.MailToList.Select(x => new MailboxAddress(x, x)));
            msg.AddTos((emailToList.Select(x => new EmailAddress(x, x)).ToList()));

            // format message text
            msg.PlainTextContent = body;
            msg.HtmlContent = body;

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);
        }
    }
}
