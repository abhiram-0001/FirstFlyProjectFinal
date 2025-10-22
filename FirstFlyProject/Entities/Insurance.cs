namespace FirstFlyProject.Entities
{
    public class Insurance
    {
        public int InsuranceId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public int BookingId { get; set; } // Foreign Key
        public string CoverageDetails { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }

        public decimal Premium { get; set; }


        // Navigation Properties (Relationships)
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}