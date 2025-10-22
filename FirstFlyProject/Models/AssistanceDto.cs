using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Models
{
    public class AssistanceDto
    {

        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolutionTime { get; set; }

    }

    public class AssistanceCreateRequest
    {
        [Required] public int UserId { get; set; }
        [Required, StringLength(1000)] public string IssueDescription { get; set; }
        public string Priority { get; set; } = "Normal"; 
    }

}
