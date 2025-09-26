using System;

namespace SKN.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; } // من 1 إلى 5
        public string Comment { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // العلاقة مع User (Many-to-One)
        public string UserId { get; set; }
        public User User { get; set; }

        // العلاقة مع Hotel (Many-to-One)
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}