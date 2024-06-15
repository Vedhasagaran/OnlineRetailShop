namespace OnlineRetailShop.Domain.Models
{
    public class Order
    {
        
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public DateTime OrderDate { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}
