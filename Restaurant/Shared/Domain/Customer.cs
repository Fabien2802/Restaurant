namespace Restaurant.Shared.Domain
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContact { get; set; }
        public string? CustomerEmail { get; set; }
        public virtual List<Reservation>? Reservations { get; set; }
    }
}
