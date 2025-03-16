using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_E_ticaretWeb.Models
{
    public class Creditcard
    {
        public int Id { get; set; }
        public string NameonCard { get; set; }
        public string KartNumber { get; set; }
        public int Cvc { get; set; }
        public int Date { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
