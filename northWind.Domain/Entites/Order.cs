namespace northWind.Domain.Entites
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; }
        public decimal Subtotal { get; set; }
        public string Year { get; set; }
    }
}
