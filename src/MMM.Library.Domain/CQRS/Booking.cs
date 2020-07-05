using MMM.Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMM.Library.Domain.CQRS
{
    public class Booking : Entity, IAggregateRoot
    {
        public int BookingCode { get; private set; }

        public Guid UserId { get; private set; }
        public DateTime RegisterDay { get; private set; }
        public EBookingStatus EBookingStatus { get; private set; }
        // TODO: calculo multa por atraso
        public decimal Fine { get; private set; }

        public Booking(Guid userId)
        {
            UserId = userId;
            RegisterDay = DateTime.Now;
            _bookinkItems = new List<BookingItem>();

            SetDraftStatus();
        }

        private readonly List<BookingItem> _bookinkItems;
        public IReadOnlyCollection<BookingItem> BookinkItems => _bookinkItems;

        public Booking() { }

        public bool AddBookingItem(BookingItem item)
        {
            if (!item.IsValid()) return false;

            item.SetBooking(Id);
            _bookinkItems.Add(item);

            return true;
        }

        public bool BookItemExists(BookingItem item)
        {
            return _bookinkItems.Any(p => p.StockId == item.StockId);
        }

        public void SetDraftStatus()
        {
            EBookingStatus = EBookingStatus.Draft;
        }
    }
}
