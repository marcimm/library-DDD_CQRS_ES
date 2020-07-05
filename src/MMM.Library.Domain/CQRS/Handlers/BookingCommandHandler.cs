using MediatR;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.CQRS.Events;
using MMM.Library.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Handlers
{
    public class BookingCommandHandler : CommandHandler,
        IRequestHandler<BookingCommandStart, bool>,
        IRequestHandler<BookingItemCommandAdd, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;

        public BookingCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler)
            : base(mediatorHandler)
        {
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
        }

        public Task<bool> Handle(BookingCommandStart request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Handle(BookingItemCommandAdd request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var booking = await _unitOfWork.BookingRepository.GetDraftBooking(request.UserId);
            var item = new BookingItem(request.StockId, request.DateStart, request.DateEnd, request.Notes);

            if (booking == null)
            {
                booking = new Booking(request.UserId);
            }
            else
            {
                if (booking.BookItemExists(item))
                {
                    await _mediatorHandler.PublishNotification(new Notification("Erro", "Item já existente na reserva!"));
                    return false;
                }
            }

            booking.AddBookingItem(item);
            _unitOfWork.BookingRepository.Add(booking);

            if (await _unitOfWork.Commit())
            {
                await _mediatorHandler.PublishEvent(new BookingItemEventAdded(booking.Id, booking.UserId, item.StockId, booking.BookingCode,
                    item.BookingItemCode, booking.RegisterDay, request.BookName, item.DateStart, item.DateEnd, item.Notes));

                return true;
            }

            await _mediatorHandler.PublishNotification(new Notification("Booking", "Erro ao registrar nova reserva!"));
            return false;
        }


    }
}
