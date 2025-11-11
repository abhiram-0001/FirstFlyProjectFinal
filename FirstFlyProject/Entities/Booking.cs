using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public int PackageId { get; set; } // Foreign Key
        public string PackageName { get; set; }
        public decimal TotalPrice { get; set; } 
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }

        // Navigation Properties (Relationships)
        public User User { get; set; } // One-to-Many with User
        public TravelPackage Package { get; set; } // One-to-Many with TravelPackage
        public ICollection<Insurance> Insurances { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}