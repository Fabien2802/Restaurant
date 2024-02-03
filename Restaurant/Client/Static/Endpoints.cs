namespace Restaurant.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string CustomersEndpoint = $"{Prefix}/customers";
        public static readonly string ItemsEndpoint = $"{Prefix}/Items";
        public static readonly string OrdersEndpoint = $"{Prefix}/Orders";
        public static readonly string ReservationsEndpoint = $"{Prefix}/reservations";
        public static readonly string SalesEndpoint = $"{Prefix}/sales";
        public static readonly string StaffsEndpoint = $"{Prefix}/staffs";
        public static readonly string TablesEndpoint = $"{Prefix}/tables";
    }
}
