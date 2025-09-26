namespace SKN.Core.DTOs
{
    public class AmenityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int UsageCount { get; set; } // عدد الفنادق التي تستخدم هذه الخدمة
    }

    public class CreateAmenityDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class UpdateAmenityDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}