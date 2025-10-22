using FirstFlyProject.Enum;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Primary Key
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; // Should be stored as a hash
        public UserRole Role { get; set; }
        public string? ContactNumber { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties (Relationships)
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AssistanceRequest> AssistanceRequests { get; set; }
        public ICollection<Insurance> Insurances { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public ICollection<CardDetail> CardDetails { get; set; }


        public Customer? Customer { get; set; }
        public TravelAgent? TravelAgent { get; set; }
    }
}