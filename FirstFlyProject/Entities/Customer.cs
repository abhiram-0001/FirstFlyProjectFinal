using FirstFlyProject.Enum;

namespace FirstFlyProject.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; } // Can be same as UserID

        public Season? season { get; set; }
        public string? EmergencyContact { get; set; }
        public User Cust { get; set; }
    }
}