using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class RoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypesWithHotelAsync()
        {
            return await _context.RoomTypes
                .Include(rt => rt.Hotel)
                .Include(rt => rt.Bookings)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> GetAvailableRoomTypesAsync(int hotelId, DateTime checkIn, DateTime checkOut)
        {
            return await _context.RoomTypes
                .Where(rt => rt.HotelId == hotelId &&
                           rt.AvailableRooms > _context.Bookings
                               .Count(b => b.RoomTypeId == rt.Id &&
                                         b.Status == "Confirmed" &&
                                         b.CheckInDate < checkOut &&
                                         b.CheckOutDate > checkIn))
                .Include(rt => rt.Hotel)
                .ToListAsync();
        }
    }
}