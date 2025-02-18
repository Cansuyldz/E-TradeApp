namespace MVC_E_ticaretWeb.Models
{
    public class Product
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }

        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
