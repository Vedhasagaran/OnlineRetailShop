using System.ComponentModel.DataAnnotations;

namespace OnlineRetailShop.Domain.DTO
{
    public class AddOrderRequestDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }
        
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }
    }
}
