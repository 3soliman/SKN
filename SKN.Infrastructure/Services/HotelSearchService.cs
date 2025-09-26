using SKN.Core.DTOs;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKN.Infrastructure.Services
{
    public class HotelSearchService : IHotelSearchService
    {
        private readonly ApplicationDbContext _context;

        public HotelSearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelDto>> SearchHotelsAsync(string searchTerm, string city, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Hotels
                .Include(h => h.HotelAmenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelImages)
                .Include(h => h.RoomTypes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(h => h.Name.Contains(searchTerm) || h.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(h => h.City.Contains(city));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(h => h.RoomTypes.Any(rt => rt.PricePerNight >= minPrice.Value));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(h => h.RoomTypes.Any(rt => rt.PricePerNight <= maxPrice.Value));
            }

            var hotels = await query.ToListAsync();

            return hotels.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Address = h.Address,
                City = h.City,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                Amenities = h.HotelAmenities.Select(ha => ha.Amenity.Name).ToList(),
                ImageUrls = h.HotelImages.Select(hi => hi.ImageUrl).ToList()
            });
        }

        public async Task<IEnumerable<HotelDto>> GetAvailableHotelsAsync(DateTime checkIn, DateTime checkOut, int guests)
        {
            // البحث عن الفنادق التي لديها غرف متاحة في التواريخ المحددة
            var bookedRoomTypeIds = await _context.Bookings
                .Where(b => !(b.CheckOutDate <= checkIn || b.CheckInDate >= checkOut) && b.Status != "Cancelled")
                .Select(b => b.RoomTypeId)
                .Distinct()
                .ToListAsync();

            var availableHotels = await _context.Hotels
                .Include(h => h.RoomTypes)
                .Include(h => h.HotelAmenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelImages)
                .Where(h => h.RoomTypes.Any(rt => 
                    !bookedRoomTypeIds.Contains(rt.Id) && 
                    rt.AvailableRooms > 0 && 
                    rt.Capacity >= guests))
                .ToListAsync();

            return availableHotels.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Address = h.Address,
                City = h.City,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                Amenities = h.HotelAmenities.Select(ha => ha.Amenity.Name).ToList(),
                ImageUrls = h.HotelImages.Select(hi => hi.ImageUrl).ToList()
            });
        }
    }
}