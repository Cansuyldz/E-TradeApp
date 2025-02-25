using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string Image { get; internal set; }
        public int Price { get; internal set; }
        public bool IsFavorite { get; internal set; }
    }
}
