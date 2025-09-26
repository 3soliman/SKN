namespace SKN.Core.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? HotelName { get; set; }
        public string? RoomTypeName { get; set; }
    }

    public class CreateBookingDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string UserId { get; set; }
        public int RoomTypeId { get; set; }
    }

    public class UpdateBookingDto
    {
        public string? Status { get; set; } // لتحديث حالة الحجز فقط
    }
}