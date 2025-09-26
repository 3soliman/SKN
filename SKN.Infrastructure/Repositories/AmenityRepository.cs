using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Amenity>> GetPopularAmenitiesAsync(int count)
        {
            return await _context.Amenities
                .Where(a => a.HotelAmenities.Count > 0)
                .OrderByDescending(a => a.HotelAmenities.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Amenity>> GetAmenitiesByCategoryAsync(string category)
        {
            return await _context.Amenities
                .Where(a => a.Name == category)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Amenity>> GetAmenitiesWithHotelsAsync()
        {
            return await _context.Amenities
                .Include(a => a.HotelAmenities)
                    .ThenInclude(ha => ha.Hotel)
                .Where(a => a.HotelAmenities.Count > 0)
                .ToListAsync();
        }
    }
}