using SKN.Core.Entities;

namespace SKN.Core.Interfaces
{
    public interface IAmenityRepository : IGenericRepository<Amenity>
    {
        Task<IEnumerable<Amenity>> GetPopularAmenitiesAsync(int count);
    }
}