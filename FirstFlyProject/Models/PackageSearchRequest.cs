namespace FirstFlyProject.Models
{
    public class PackageSearchRequest
    {
        public int PackageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int TravelAgentID { get; set; }
        
        // Calculated property (the total price for the group)
        public decimal TotalGroupPrice { get; set; }
        
    }
}