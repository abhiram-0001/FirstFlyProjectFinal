using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public int BookingId { get; set; } // Foreign Key
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation Properties (Relationships)
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}