using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    public class FavoriteController : Controller
    {
        DataBaseContext _context = new();
        public IActionResult Favorite()
        {
            var userMail = HttpContext.Session.GetString("mail");
            if (string.IsNullOrEmpty(userMail))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var favoriteProducts = _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.Product)
                .Select(f => new ProductViewModel
                {
                    Id = f.Product.Id,
                    Name = f.Product.Name,
                    Image = f.Product.Image,
                    Price = f.Product.Price,
                    IsFavorite = true // Zaten favorilerde olduğu için true gönderiyoruz
                }).ToList();

            return View(favoriteProducts);
        }





        // Favori ürünü kaldırma
        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            var favorite = await _context.Favorites.FindAsync(favoriteId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        private int GetCurrentUserId()
        {
            // Kullanıcının ID'sini çek (Projene uygun şekilde düzenle)
            return 1; // Örnek olarak ID 1 döndürüyor, kendi login sistemine göre değiştir.
        }



        [HttpPost]
        public IActionResult AddToFavorites(int productId)
        {
            var userMail = HttpContext.Session.GetString("mail");
            if (string.IsNullOrEmpty(userMail))
            {
                return Json(new { success = false, message = "Oturum bulunamadı!" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı!" });
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Ürün bulunamadı!" });
            }

            // Daha önce eklenmiş mi kontrol edelim
            var existingFavorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == user.Id && f.ProductId == productId);

            if (existingFavorite != null)
            {
                return Json(new { success = false, message = "Bu ürün zaten favorilerde!" });
            }

            // Favorilere ekleyelim
            var favorite = new Favorite
            {
                UserId = user.Id,
                ProductId = productId
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            Console.WriteLine($"Favorilere eklendi: Kullanıcı {user.Id}, Ürün {productId}"); // Log ekleme

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult ToggleFavorite(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false, message = "Lütfen giriş yapın." });

            var favorite = _context.Favorites.FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                // Favoriden kaldır
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
                return Json(new { isFavorited = false });
            }
            else
            {
                // Favoriye ekle
                var newFavorite = new Favorite { UserId = userId.Value, ProductId = productId };
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
                return Json(new { isFavorited = true });
            }
        }

    }
}
