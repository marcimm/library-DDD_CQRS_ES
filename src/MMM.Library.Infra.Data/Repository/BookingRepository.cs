using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.CQRS;
using MMM.Library.Domain.CQRS.Interfaces;
using MMM.Library.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(LibraryDbContext context) : base(context)
        { }

        public async Task<Booking> GetDraftBooking(Guid userId)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(p => 
                p.UserId == userId && p.EBookingStatus == EBookingStatus.Draft);

            if (booking == null) return null;

            await _dbContext.Entry(booking).Collection(p => p.BookinkItems).LoadAsync();

            return booking;
        }
    }
}
