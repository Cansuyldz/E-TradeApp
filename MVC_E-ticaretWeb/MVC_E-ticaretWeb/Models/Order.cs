using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        // Navigation property
        public List<OrderProducts> OrderProducts { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
