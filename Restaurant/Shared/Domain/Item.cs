namespace Restaurant.Shared.Domain
{
    public class Item
    {
        public int ItemID { get; set; }
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string? ItemMenu { get; set; }
        public int ItemQuantity { get; set; }
    }
}