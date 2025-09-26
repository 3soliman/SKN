using SKN.Core.Entities;
using SKN.Core.Interfaces;

namespace SKN.Core.Interface
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsWithDetailsAsync();
    }
}