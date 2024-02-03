namespace Restaurant.Shared.Domain
{
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime ReservationDateCreated { get; set; }
        public DateTime ReservationDateUpdated { get; set; }
        public double SaleTotalAmount { get; set; }

        public int OrderID { get; set; }
        public virtual Order? Orders { get; set; }

    }
}
