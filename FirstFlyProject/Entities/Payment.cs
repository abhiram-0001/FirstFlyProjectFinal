using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public int BookingId { get; set; } // Foreign Key
        public float Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }

        // Navigation Properties (Relationships)
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}