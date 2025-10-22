namespace FirstFlyProject.Models
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int PackageId { get; set; }
        public string Status { get; set; } = "Not Booked";
        public decimal price { get; set; }

    }
}
