namespace FirstFlyProject.Models
{
    public class CreateBookingRequest
    {
        public int PackageID { get; set; }
        public DateTime SelectedStartDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IncludeInsurance { get; set; } 
    }
}
