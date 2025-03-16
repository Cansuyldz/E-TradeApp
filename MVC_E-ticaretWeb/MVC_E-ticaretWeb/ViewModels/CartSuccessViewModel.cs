using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class CartSuccessViewModel
    {
        public string NameonCard { get; set; }
        public string KartNumber { get; set; }
        public int Cvc { get; set; }
        public int Date { get; set; }
        public int Year { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
