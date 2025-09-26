namespace SKN.Core.Entities
{
    public class HotelImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; } // صورة أساسية للفندق
        public int DisplayOrder { get; set; } // ترتيب العرض

        // العلاقة مع Hotel
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}