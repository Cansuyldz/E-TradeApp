using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }  
        public string AddressLine { get; set; } 
        public string City { get; set; }  
        public string PostalCode { get; set; }  
        public string Country { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string NewField { get; set; }
        public int New { get; set; }
    }

}
