using MediatR;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.CQRS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Handlers
{
    public class BookingEventHandler :
        INotificationHandler<BookingItemEventAdded>
    {
        private readonly IEmailService _emailService;
        private readonly IUser _user;

        public BookingEventHandler(IEmailService emailService, IUser user)
        {
            _emailService = emailService;
            _user = user;

        }

        public async Task Handle(BookingItemEventAdded notification, CancellationToken cancellationToken)
        {
            var email = await _user.GetUserName();
            var subject = "Nova reserva do livro: " + notification.BookName;
            var messageHtml = @"<html>
                      <body>
                      <p>Prezado</p>
                      <p>Seu livro foi reservado com sucesso através do nosso sitema eletrônico</p>.< /br> 
                      < /br>
                      <p>Dados da Reserva: </p>< /br>"
                      + notification.BookName + "< /br>"
                      + notification.DateStart + "< /br>"
                      + notification.DateEnd + "< /br>"

                      + "</body></html>";

           // _emailService.SendEmailAsync(email, subject, messageHtml);
        }
    }
}
