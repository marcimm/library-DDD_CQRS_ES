using MMM.Library.Domain.Core.Models;
using System;

namespace MMM.Library.Domain.CQRS
{
    public class BookingItem : Entity
    {
        public BookingItem(Guid stockId, DateTime dateStart, DateTime dateEnd, string notes)
        {
            StockId = stockId;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Notes = notes;
        }

        public Guid StockId { get; private set; }
        public Guid BookingId { get; private set; }
        public int BookingItemCode { get; private set; }

        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public string Notes { get; private set; }

        // EF
        public BookingItem() { }
        public Booking Booking { get; private set; }


        public void SetBooking(Guid bookingId)
        {
            BookingId = bookingId;
        }
    }
}
