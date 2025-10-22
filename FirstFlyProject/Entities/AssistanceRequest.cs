using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class AssistanceRequest
    {
        [Key]
        public int RequestId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public string IssueDescription { get; set; }
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolutionTime { get; set; }

        public string Priority { get; set; } = "Normal"; // "High" for emergencies
        public string Channel { get; set; } = "Chat";

        // Navigation Properties (Relationships)
        public User User { get; set; }
    }
}
