using System;

namespace SKN.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // العلاقة مع User (Many-to-One)
        public int UserId { get; set; }
        public User User { get; set; }

        // العلاقة مع RoomType (Many-to-One)
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
    }
}