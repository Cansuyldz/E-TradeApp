using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.Models
{
    public class OrderProducts
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for Cart
        [Required]
        public int OrderId { get; set; }

        // Foreign key for Product
        [Required]
        public int ProductId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public decimal BasePrice { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }
    }
}