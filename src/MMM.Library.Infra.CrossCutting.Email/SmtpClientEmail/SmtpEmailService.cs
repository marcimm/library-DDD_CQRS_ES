using KissLog;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MMM.Library.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Email.SmtpClientEmail
{
    public class SmtpEmailService : IEmailService
    {
        public SmtpConfiguration SmtpConfig { get; } //set only via Secret Manager
        private readonly ILogger _logger;

        public SmtpEmailService(IOptions<SmtpConfiguration> optionsAccessor, ILogger logger)
        {
            SmtpConfig = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task<string> SendEmailAsync(IEnumerable<string> mailToList, string mailFrom,
            string subject, string body, bool isBodyHtml)
        {
            var message = new MimeMessage();
            message.To.AddRange(mailToList.Select(x => new MailboxAddress(x, x)));

            if (String.IsNullOrEmpty(mailFrom))
            {
                message.From.Add(new MailboxAddress(SmtpConfig.From, SmtpConfig.From));
            }
            else
            {
                message.From.Add(new MailboxAddress(mailFrom, mailFrom));
            }
            message.Subject = subject;

            // Text Format: HTML or plain
            if (isBodyHtml == true)
            {
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };
            }
            else
            {
                message.Body = new TextPart(TextFormat.Plain)
                {
                    Text = body
                };
            }

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(SmtpConfig.SmtpServer, SmtpConfig.Port, SecureSocketOptions.StartTls);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.SslProtocols = SslProtocols.Tls;
                emailClient.Authenticate(SmtpConfig.UserName, SmtpConfig.Password);

                await emailClient.SendAsync(message);

                emailClient.Disconnect(true);
            }

            return string.Concat("Email sent to: ") + string.Join(" ", mailToList.ToList());
        }
    }

}

