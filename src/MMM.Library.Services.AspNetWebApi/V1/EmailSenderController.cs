using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/email")]
    public class EmailSenderController : ApiBaseController
    {
        private readonly IEmailService _emailSender;
        public EmailSenderController(INotificationHandler<Notification> notifications,
                                     IMediatorHandler mediatorHandler,
                                     IEmailService emailSender)
          : base(notifications, mediatorHandler)
        {
            _emailSender = emailSender;
        }


        [HttpPost]
        [Route("send-email")]
        public async Task<string> SendEmailAsync(EmailMessageViewModel email)
        {

            return await
                 _emailSender.SendEmailAsync(email.MailToList, email.MailFrom, email.Subject, email.Body, email.IsBodyHtml);

        }

        [HttpPost]
        [Route("send-test-email")]
        public async Task<ActionResult<string>> SendTestEmail()
        {
            var mailToList = new List<string>(new string[] { "marcio.molina.m@gmail.com" });

            var subject = "Email Test - Library System";
            var msgHtml = new StringBuilder();
            msgHtml.Append("<html><body><h3><strong>Library System</strong></h3>");
            msgHtml.Append("<H5>Email gerado automaticamete por rotina de teste<H5>");
            msgHtml.Append("<br><br><p>Informações:</p>");
            msgHtml.Append("<br>Data: " + DateTime.Now + "</p>");
            //msgHtml.Append("</br>Usuário: "  "</p>");
            msgHtml.Append("</body></html>");

            var emailViewModel = new EmailMessageViewModel(mailToList, "", subject, msgHtml.ToString(), true);

            return CustomResponse(await SendEmailAsync(emailViewModel));
        }
    }
}
