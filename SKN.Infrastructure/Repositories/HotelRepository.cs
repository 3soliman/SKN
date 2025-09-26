using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Hotel>> GetHotelsWithDetailsAsync()
        {
            return await _context.Hotels
                .Include(h => h.RoomTypes)
                .Include(h => h.HotelAmenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelImages)
                .Include(h => h.Reviews)
                .ToListAsync();
        }

        // أضف الدوال المفقودة
        public async Task<Hotel> GetHotelWithDetailsAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.RoomTypes)
                .Include(h => h.HotelAmenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelImages)
                .Include(h => h.Reviews)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city)
        {
            return await _context.Hotels
                .Where(h => h.City.ToLower() == city.ToLower())
                .Include(h => h.HotelImages)
                .Include(h => h.Reviews)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetHotelsWithAmenitiesAsync()
        {
            return await _context.Hotels
                .Where(h => h.HotelAmenities.Any())
                .Include(h => h.HotelAmenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelImages)
                .ToListAsync();
        }
    }
}