using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Models
{
    public class CardPaymentDto
    {
        [Required]
        public int CardDetailId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public float Amount { get; set; }

        // Card Details for processing/validation
        [Required]
        public string CardNumber { get; set; }

        [Required, StringLength(100)]
        public string CardHolderName { get; set; }

        [Required, RegularExpression(@"^(0[1-9]|1[0-2])$", ErrorMessage = "Invalid Month (MM)")]
        public string ExpiryMonth { get; set; }

        [Required, RegularExpression(@"^\d{2}$", ErrorMessage = "Invalid Year (YY)")]
        public string ExpiryYear { get; set; }

        [Required, StringLength(4), RegularExpression(@"^\d{3}$")]
        public string CVV { get; set; } // Used for validation

        // Optional: Whether to save the card for future use
        public bool SaveCard { get; set; }
    }
    public class upiPaymentDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)] 
        public float Amount { get; set; }
        [Required]
        [StringLength(100)]
        public string UpiId {  get; set; }
    }
    public class PaymentResultDto
    {
        public bool success { get; set; }
        public int? paymentId { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
        public string paymentMethod { get; set; }
    }
}
