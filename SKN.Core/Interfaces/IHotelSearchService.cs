using SKN.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKN.Core.Interfaces
{
    public interface IHotelSearchService
    {
        Task<IEnumerable<HotelDto>> SearchHotelsAsync(string searchTerm, string city, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<HotelDto>> GetAvailableHotelsAsync(DateTime checkIn, DateTime checkOut, int guests);
    }
}