using System.ComponentModel.DataAnnotations;

namespace SKN.Core.Entities
{
    public class HotelAmenity
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}