namespace SKN.Core.DTOs
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int AvailableRooms { get; set; }
        public string HotelName { get; set; }
        public int HotelId { get; set; }
    }

    public class CreateRoomTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int AvailableRooms { get; set; }
        public int HotelId { get; set; }
    }

    public class UpdateRoomTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int AvailableRooms { get; set; }
    }
}