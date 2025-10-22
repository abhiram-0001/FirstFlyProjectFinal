using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Models
{
    public class InsuranceOptionDto
    {

        public string Provider { get; set; } = "DemoInsure";
        public string CoverageDetails { get; set; } = "Medical ₹5L, Baggage ₹50K, Delay ₹10K";
        public decimal PremiumPerPerson { get; set; } = 50.00m;

    }

    public class InsuranceSelectionRequest
    {
        [Required] public int BookingId { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public bool IncludeInsurance { get; set; }
        [Range(1, int.MaxValue)] public int NumberOfPeople { get; set; }
    }

    public class InsuranceResultDto
    {
        public bool Success { get; set; }
        public int? InsuranceId { get; set; }
        public string Status { get; set; }         // "Active" | "Pending Payment" etc.
        public decimal Premium { get; set; }
        public string Message { get; set; }
    }

}
