using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKN.Core.Entities;

namespace SKN.Infrastructure.Data1
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets للإضافة إلى Identity
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // تكوين العلاقات الخاصة بك
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            
            // تكوين إعدادات Identity الإضافية إذا لزم الأمر
            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.PhoneNumber).HasMaxLength(20);
            });
        }
    }
}