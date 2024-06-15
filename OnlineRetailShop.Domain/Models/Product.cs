using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRetailShop.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]     
        public decimal Price { get; set; }
 
        public int Quantity { get; set; }

    }
}
