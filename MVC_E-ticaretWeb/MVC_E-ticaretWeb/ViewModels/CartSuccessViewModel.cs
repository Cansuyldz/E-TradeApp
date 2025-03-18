using MVC_E_ticaretWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class CartSuccessViewModel
    {
        public string NameonCard { get; set; }

        public string KartNumber { get; set; }
        public string Cvc { get; set; }
        public string Date { get; set; }
        public string Year { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
