using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.Models
{
    public class Adress
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Streetaddress { get; set; }//açıkadres
        public string AddressLine { get; set; } //adres başlığı
        public string Province { get; set; } //il
        public string District { get; set; }//ilçe
        public string Neighbourhood { get; set; } //Mahalle

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public bool IsSelected { get; internal set; }
    }
}
