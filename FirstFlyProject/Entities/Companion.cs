
using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class Companion
    {
        [Key]
        public int CompanionId { get; set; } // Primary Key
        public int UserId { get; set; } //Foreign Key
        public string Gender { get; set; } 
        public int Age { get; set; }
        public string Name { get; set; }

        // Navigation Properties (Relationships)
        public User User { get; set; }
    }
}