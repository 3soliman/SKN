using System.Collections.Generic;

namespace SKN.Core.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; } // خط الطول للموقع
        public decimal Longitude { get; set; } // خط العرض للموقع

        // علاقة one-to-many مع RoomTypes
        public ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();
        
        // علاقة one-to-many مع Reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        
        // علاقة one-to-many مع HotelImages
        public ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
        
        // علاقة many-to-many مع Amenities
        public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
    }
}