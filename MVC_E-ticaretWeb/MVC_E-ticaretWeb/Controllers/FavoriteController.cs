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
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var favoriteProducts = _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.Product)
                .Select(f => new ProductViewModel
                {
                    Id = f.Product.Id,
                    Name = f.Product.Name,
                    Image = f.Product.Image,
                    Price = f.Product.Price,
                    IsFavorite = true
                }).ToList();

            Console.WriteLine($"Favorilerdeki ürün sayısı: {favoriteProducts.Count}");

            return View(favoriteProducts);
        }

        [HttpGet]
        public IActionResult CheckFavorite(int id)
        {
            var userMail = HttpContext.Session.GetString("mail");
            if (string.IsNullOrEmpty(userMail))
                return Json(new { isFavorite = false });

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
                return Json(new { isFavorite = false });

            var isFavorite = _context.Favorites.Any(f => f.UserId == user.Id && f.ProductId == id);
            return Json(new { isFavorite });
        }

        public IActionResult RemoveFromFavorites(int id)
        {
            try
            {
                var mail = HttpContext.Session.GetString("mail");
                if (string.IsNullOrEmpty(mail))
                {
                    return Json(new { success = false, message = "Oturum açık değil." });
                }

                var user = _context.Users.FirstOrDefault(u => u.Mail == mail);
                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                var favorite = _context.Favorites.FirstOrDefault(f => f.UserId == user.Id && f.ProductId == id);
                if (favorite == null)
                {
                    return Json(new { success = false, message = "Bu ürün zaten favorilerde değil." });
                }

                _context.Favorites.Remove(favorite);

                try
                {
                    _context.SaveChanges();
                    return Json(new { isFavorite = false, message = "Ürün favorilerden kaldırıldı." });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Json(new { success = false, message = "Favoriler güncellenirken bir hata oluştu, lütfen tekrar deneyin." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ToggleFavorite(int id)
        {
            var userMail = HttpContext.Session.GetString("mail");
            if (string.IsNullOrEmpty(userMail))
                return Json(new { success = false, message = "Lütfen giriş yapın." });

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
                return Json(new { success = false, message = "Kullanıcı bulunamadı!" });

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return Json(new { success = false, message = "Ürün bulunamadı!" });

            var favorite = _context.Favorites.FirstOrDefault(f => f.UserId == user.Id && f.ProductId == id);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
                return Json(new { isFavorite = false, message = "Ürün favorilerden kaldırıldı." });
            }
            else
            {
                // **EKLEME ÖNCESİ TEKRAR KONTROLÜ**
                var alreadyExists = _context.Favorites.Any(f => f.UserId == user.Id && f.ProductId == id);
                if (!alreadyExists)
                {
                    var newFavorite = new Favorite
                    {
                        UserId = user.Id,
                        ProductId = id
                    };

                    _context.Favorites.Add(newFavorite);
                    _context.SaveChanges();

                    // **EKLEME KONTROLÜ (Gerçekten kaydedilmiş mi?)**
                    var checkFavorite = _context.Favorites.Any(f => f.UserId == user.Id && f.ProductId == id);
                    if (checkFavorite)
                    {
                        return Json(new { isFavorite = true, message = "Ürün favorilere eklendi!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Favorilere ekleme başarısız!" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Ürün zaten favorilerde!" });
                }
            }
        }


    }
}

