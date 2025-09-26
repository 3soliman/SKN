namespace SKN.Core.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
         public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string HotelName { get; set; }
        public string UserId { get; set; }
        public int HotelId { get; set; }
    }

    public class CreateReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int HotelId { get; set; }
    }

    public class UpdateReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}