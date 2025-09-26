namespace SKN.Core.Entities
{
    public class HotelAmenity
    {
        // العلاقة مع Hotel (Many-to-One)
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        // العلاقة مع Amenity (Many-to-One)
        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}