using MVC_E_ticaretWeb.Migrations;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class CartAddressViewModel
    {
        public Cart Cart { get; set; } // Liste yerine tek bir Cart
        public List<Adress> Adresses { get; set; }

    }
}
