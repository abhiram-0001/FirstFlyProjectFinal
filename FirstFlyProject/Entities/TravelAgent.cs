using System.ComponentModel.DataAnnotations;

namespace FirstFlyProject.Entities
{
    public class TravelAgent
    {
        [Key]
        public int TravelAgentID { get; set; } // Can be same as UserID
        public string? AgencyName { get; set; }
        public bool? IsVerified { get; set; }

        public User Agent { get; set; }

        public ICollection<TravelPackage> TravelPackages { get; set; }
    }
}