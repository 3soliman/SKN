using SKN.Core.Entities;

namespace SKN.Core.Interfaces
{
    public interface IHotelImageRepository : IGenericRepository<HotelImage>
    {
        Task<IEnumerable<HotelImage>> GetImagesWithHotelAsync();
        Task<IEnumerable<HotelImage>> GetImagesByHotelAsync(int hotelId);
        Task<HotelImage> GetPrimaryImageAsync(int hotelId);
        Task SetPrimaryImageAsync(int imageId);
    }
}