namespace UnitTestingUsingMoq.Models
{
    public class Order
    {
        //Order: OrderId, UserId, Product, Quantity, Price
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Product {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //navigation 
        public User User { get; set; }
    }
}
