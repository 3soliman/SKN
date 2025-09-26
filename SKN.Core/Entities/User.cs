using System;
using System.Collections.Generic;

namespace SKN.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // علاقة one-to-many مع Bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        
        // علاقة one-to-many مع Reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}