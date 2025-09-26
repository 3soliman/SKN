using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class HotelImageRepository : GenericRepository<HotelImage>, IHotelImageRepository
    {
        public HotelImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HotelImage>> GetImagesWithHotelAsync()
        {
            return await _context.HotelImages
                .Include(hi => hi.Hotel)
                .OrderBy(hi => hi.HotelId)
                .ThenBy(hi => hi.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<HotelImage>> GetImagesByHotelAsync(int hotelId)
        {
            return await _context.HotelImages
                .Where(hi => hi.HotelId == hotelId)
                .OrderBy(hi => hi.DisplayOrder)
                .ToListAsync();
        }

        public async Task<HotelImage> GetPrimaryImageAsync(int hotelId)
        {
            return await _context.HotelImages
                .FirstOrDefaultAsync(hi => hi.HotelId == hotelId && hi.IsPrimary);
        }

        public async Task SetPrimaryImageAsync(int imageId)
        {
            var image = await _context.HotelImages
                .Include(hi => hi.Hotel)
                .FirstOrDefaultAsync(hi => hi.Id == imageId);

            if (image != null)
            {
                // إلغاء الصور الأساسية الأخرى لنفس الفندق
                var existingPrimaryImages = await _context.HotelImages
                    .Where(hi => hi.HotelId == image.HotelId && hi.IsPrimary && hi.Id != imageId)
                    .ToListAsync();

                foreach (var primaryImage in existingPrimaryImages)
                {
                    primaryImage.IsPrimary = false;
                }

                image.IsPrimary = true;
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
