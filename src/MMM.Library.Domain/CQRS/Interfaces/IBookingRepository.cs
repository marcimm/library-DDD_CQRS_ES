using MMM.Library.Domain.Core.Data;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Domain.CQRS.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetDraftBooking(Guid userId);
    }
}
