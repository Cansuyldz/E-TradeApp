using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/profile")]
    public class ProfileController : BaseController
    {
        [HttpGet("/profile/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
        [HttpGet("/profile/favorilerim")]
        public IActionResult Favorite()
        {
            var userMail = HttpContext.Session.GetString("mail");
            if (string.IsNullOrEmpty(userMail))
                return RedirectToAction("Login", "Authorize");

            var user = _context.Users.FirstOrDefault(u => u.Mail == userMail);
            if (user == null)
                return RedirectToAction("Login", "Authorize");

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
    }
}
