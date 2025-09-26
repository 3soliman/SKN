using SKN.Core.Entities;

namespace SKN.Core.Interfaces
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        Task<IEnumerable<RoomType>> GetRoomTypesWithHotelAsync();
        Task<IEnumerable<RoomType>> GetAvailableRoomTypesAsync(int hotelId, DateTime checkIn, DateTime checkOut);
    }
}