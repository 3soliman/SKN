using SKN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKN.Core.Interfaces{
public interface IHotelRepository : IGenericRepository<Hotel>
{
    Task<IEnumerable<Hotel>> GetHotelsWithDetailsAsync();
    Task<Hotel> GetHotelWithDetailsAsync(int id);        // هذه مفقودة
    Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city); // هذه مفقودة
    Task<IEnumerable<Hotel>> GetHotelsWithAmenitiesAsync(); // هذه مفقودة
}
}