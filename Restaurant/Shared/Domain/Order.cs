namespace Restaurant.Shared.Domain
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDateCreated { get; set; }
        public DateTime OrderDateUpdated { get; set; }


        //public int StaffID { get; set; }
        //public virtual Staff? Staffs { get; set; }

        public int ItemID { get; set; }
        public virtual Item? Items { get; set; }

        public int ReservationID { get; set; }
        public virtual Reservation? Reservations { get; set; }
    }
}
