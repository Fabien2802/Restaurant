namespace Restaurant.Shared.Domain
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDateCreated { get; set; }
        public DateTime ReservationDateUpdated { get; set; }
        public string? ReservationTime { get; set; }
        public string? ReservationStatus { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer? Customer { get; set; }

        public int StaffID { get; set; }
        public virtual Staff? Staff { get; set; }

        public int TableID { get; set; }
        public virtual Table? Table { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}
