using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class CardDetail
    {
        [Key]
        public int CardDetailId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public string CardNumber { get; set; } // Hashed/Masked in real life
        public string CardHolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}