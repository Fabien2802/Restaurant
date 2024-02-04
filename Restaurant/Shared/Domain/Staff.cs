namespace Restaurant.Shared.Domain
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string? StaffName { get; set; }
        public string? StaffContact { get; set; }
        public string? StaffEmail { get; set; }
        public int StaffRole { get; set; } //If StaffRole = 1, Normal Staff,  If StaffRole = 2, Manager
                                           //Normal Staff can only view Reservations
    }
}
