using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_E_ticaretWeb.Models
{
    public class Product
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        [NotMapped] // Bu alan veritabanına kaydedilmeyecek
        public IFormFile? ImageFile { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [NotMapped]
        public bool IsFavorite { get; set; }

        public ICollection<CartProduct>? CartProducts { get; set; }
    }
}
