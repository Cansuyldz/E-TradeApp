using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_E_ticaretWeb.Models
{
    public class Creditcard
    {
        public int Id { get; set; }
        [Required]
        public string NameonCard { get; set; }
        [Required]
        public  string KartNumber { get; set; }
        public string Cvc { get; set; }
        public string Date { get; set; }
        public string Year { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
