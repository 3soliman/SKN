using SKN.Core.Entities;

namespace SKN.Infrastructure.Data1
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { FirstName = "سليمان", LastName = "العمري", Email = "sulaiman@skn.com", PhoneNumber = "0551234567" },
                    new User { FirstName = "أحمد", LastName = "محمد", Email = "ahmed@skn.com", PhoneNumber = "0557654321" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Amenities.Any())
            {
                var amenities = new List<Amenity>
                {
                    new Amenity { Name = "واي فاي مجاني", Icon = "wifi" },
                    new Amenity { Name = "مسبح", Icon = "pool" },
                    new Amenity { Name = "موقف سيارات", Icon = "parking" },
                    new Amenity { Name = "مطعم", Icon = "restaurant" },
                    new Amenity { Name = "جيم", Icon = "gym" }
                };

                context.Amenities.AddRange(amenities);
                context.SaveChanges();
            }

            if (!context.Hotels.Any())
            {
                var hotel = new Hotel
                {
                    Name = "فندق SKN الفاخر",
                    Description = "فندق 5 نجوم مع جميع الخدمات",
                    Address = "شارع الملك فهد، الرياض",
                    City = "الرياض",
                    Latitude = 24.7136m,
                    Longitude = 46.6753m
                };

                context.Hotels.Add(hotel);
                context.SaveChanges();

                // إضافة المرافق للفندق
                var hotelAmenities = new List<HotelAmenity>
                {
                    new HotelAmenity { HotelId = hotel.Id, AmenityId = 1 },
                    new HotelAmenity { HotelId = hotel.Id, AmenityId = 2 },
                    new HotelAmenity { HotelId = hotel.Id, AmenityId = 3 },
                    new HotelAmenity { HotelId = hotel.Id, AmenityId = 4 },
                    new HotelAmenity { HotelId = hotel.Id, AmenityId = 5 }
                };

                context.HotelAmenities.AddRange(hotelAmenities);
                context.SaveChanges();

                // إضافة أنواع الغرف
                var roomTypes = new List<RoomType>
                {
                    new RoomType { Name = "غرفة ديلوكس", Description = "غرفة فاخرة مع إطلالة رائعة", PricePerNight = 450, Capacity = 2, AvailableRooms = 10, HotelId = hotel.Id },
                    new RoomType { Name = "سويت", Description = "جناح فاخر مع صالة مستقلة", PricePerNight = 800, Capacity = 4, AvailableRooms = 5, HotelId = hotel.Id }
                };

                context.RoomTypes.AddRange(roomTypes);
                context.SaveChanges();
            }
            // إضافة تقييمات تجريبية
if (!context.Reviews.Any())
{
    var reviews = new List<Review>
    {
        new Review { Rating = 5, Comment = "فندق رائع وخدمات ممتازة", UserId = 1, HotelId = 1 },
        new Review { Rating = 4, Comment = "جيد ولكن يحتاج تحسين في السرعة", UserId = 2, HotelId = 1 }
    };

    context.Reviews.AddRange(reviews);
    context.SaveChanges();
}

// إضافة حجوزات تجريبية
if (!context.Bookings.Any())
{
    var bookings = new List<Booking>
    {
        new Booking { 
            CheckInDate = DateTime.Now.AddDays(7), 
            CheckOutDate = DateTime.Now.AddDays(10), 
            NumberOfGuests = 2, 
            TotalPrice = 1350, 
            Status = "Confirmed",
            UserId = 1, 
            RoomTypeId = 1 
        }
    };

    context.Bookings.AddRange(bookings);
    context.SaveChanges();
}
// إضافة صور للفندق
if (!context.HotelImages.Any())
{
    var hotelImages = new List<HotelImage>
    {
        new HotelImage { 
            ImageUrl = "https://example.com/images/hotel1.jpg", 
            AltText = "واجهة الفندق الرئيسية", 
            IsPrimary = true, 
            DisplayOrder = 1, 
            HotelId = 1 
        },
        new HotelImage { 
            ImageUrl = "https://example.com/images/hotel2.jpg", 
            AltText = "غرفة النوم الفاخرة", 
            IsPrimary = false, 
            DisplayOrder = 2, 
            HotelId = 1 
        }
    };

    context.HotelImages.AddRange(hotelImages);
    context.SaveChanges();
}
        }
    }
}