using SKN.Core.Entities;

namespace SKN.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsWithUserAsync(int hotelId);
        Task<IEnumerable<Review>> GetRecentReviewsAsync(int count);
        Task<double> GetAverageRatingAsync(int hotelId);
        Task<int> GetReviewCountAsync(int hotelId);
        Task<IEnumerable<Review>> GetVerifiedReviewsAsync(int hotelId);
    }
}