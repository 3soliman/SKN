using System.Collections.Generic;

namespace SKN.Core.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; } // مثل: "غرفة ديلوكس", "سويت"
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; } // عدد الأشخاص
        public int AvailableRooms { get; set; } // عدد الغرف المتاحة من هذا النوع

        // العلاقة مع Hotel (Many-to-One)
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        // العلاقة مع Bookings (One-to-Many)
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}