using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        // Navigation property
        public List<CartProduct> CartProducts { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

    }
}
