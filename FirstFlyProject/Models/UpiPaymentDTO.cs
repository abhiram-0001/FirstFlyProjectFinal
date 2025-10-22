using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Models
{
    public class UpiPaymentDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public float Amount { get; set; }

        // UPI Identifier (VPA)
        [Required, RegularExpression(@"^[\w.-]+@[\w.-]+$", ErrorMessage = "Invalid VPA format")]
        public string VPA { get; set; } // Virtual Payment Address (e.g., user@bank)
    }
}
