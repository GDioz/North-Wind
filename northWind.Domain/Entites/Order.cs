namespace northWind.Domain.Entites
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; }
    }
}
