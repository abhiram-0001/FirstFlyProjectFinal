using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class TravelPackage
    {
        [Key]
        public int PackageId { get; set; } // Primary Key
        public string Title { get; set; }
        public string ?Destination { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public float? Price { get; set; }
        public string? IncludedServices { get; set; }
        public string ? ImageUrl { get; set; }
        public int TravelAgentID { get; set; }

        // Navigation Properties (Relationships)
        public ICollection<Booking> Bookings { get; set; }
        public TravelAgent Agent { get; set; }
    }
}