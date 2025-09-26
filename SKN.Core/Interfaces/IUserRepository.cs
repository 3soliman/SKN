using SKN.Core.Entities;

namespace SKN.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersWithBookingsAsync();
    }
}