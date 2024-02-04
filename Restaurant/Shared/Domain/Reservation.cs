using System.ComponentModel.DataAnnotations;

namespace Restaurant.Shared.Domain
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDateCreated { get; set; }
        public DateTime ReservationDateUpdated { get; set; }

        public string? ReservationTime { get; set; }

        public DateTime ReservationDateChoice { get; set; }
        //public int CustomerID { get; set; }
        //public virtual Customer? Customer { get; set; }

        public int TableID { get; set; }
        public virtual Table? Table { get; set; }

    }
}
