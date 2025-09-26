using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Infrastructure.Data1;
using SKN.Core.Interface;

namespace SKN.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        // استخدم base(context) بدلاً من الوصول المباشر لـ _context
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetBookingsWithDetailsAsync()
        {
            // استخدم DbContext من GenericRepository عبر خاصية protected
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.RoomType)
                    .ThenInclude(rt => rt.Hotel)
                .ToListAsync();
        }
    }
}