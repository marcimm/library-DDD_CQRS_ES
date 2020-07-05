using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Events
{
    public class BookingItemEventAdded : Event
    {
        public Guid BookingId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid StockId { get; private set; }
        public int BookingCode { get; private set; }
        public int BookingItemCode { get; private set; }
        public DateTime RegisterDay { get; private set; }
        public string BookName { get; private set; }
        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public string Notes { get; private set; }

        public BookingItemEventAdded(Guid bookingId, Guid userId, Guid stockId, int bookingCode, int bookingItemCode, 
            DateTime registerDay, string bookName, DateTime dateStart, DateTime dateEnd, string notes)
        {
            BookingId = bookingId;
            UserId = userId;
            StockId = stockId;
            BookingCode = bookingCode;
            BookingItemCode = bookingItemCode;
            RegisterDay = registerDay;
            BookName = bookName;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Notes = notes;
        }
    }
}
