namespace SKN.Core.DTOs
{
    public class HotelImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
    }

    public class CreateHotelImageDto
    {
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
        public int HotelId { get; set; }
    }

    public class UpdateHotelImageDto
    {
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}