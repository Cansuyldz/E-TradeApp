using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class CartAddressViewModel
    {
        public Cart Cart { get; set; } 
        public List<Product> Products { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public List<Address> Adresses { get; set; }
        public List<Creditcard> Creditcards { get; set; }

    }
}
