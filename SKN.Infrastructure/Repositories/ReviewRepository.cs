using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewsWithUserAsync(int hotelId)
        {
            return await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetRecentReviewsAsync(int count)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Hotel)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int hotelId)
        {
            var averageRating = await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .AverageAsync(r => (double?)r.Rating) ?? 0.0;

            return Math.Round(averageRating, 1);
        }

        public async Task<int> GetReviewCountAsync(int hotelId)
        {
            return await _context.Reviews
                .CountAsync(r => r.HotelId == hotelId);
        }

        public async Task<IEnumerable<Review>> GetVerifiedReviewsAsync(int hotelId)
        {
            return await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
        public new async Task<int> SaveChangesAsync()
{
    return await _context.SaveChangesAsync();
}
    }
}