using System.Collections.Generic;

namespace SKN.Core.Entities
{
    public class Amenity
    {
        public int Id { get; set; }
        public string? Name { get; set; } // مثل: "واي فاي", "مسبح", "موقف سيارات"
        public string? Icon { get; set; } // اسم أيقونة (اختياري)

        // العلاقة مع HotelAmenities (One-to-Many)
        public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
    }
}