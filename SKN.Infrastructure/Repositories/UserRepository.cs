using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;
using SKN.Core.Interfaces;
using SKN.Infrastructure.Data1;

namespace SKN.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersWithBookingsAsync()
        {
            return await _context.Users
                .Include(u => u.Bookings)
                    .ThenInclude(b => b.RoomType)
                        .ThenInclude(rt => rt.Hotel)
                .ToListAsync();
        }
    }
}