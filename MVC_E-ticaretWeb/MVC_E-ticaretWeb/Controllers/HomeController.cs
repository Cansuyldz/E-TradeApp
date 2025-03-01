using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/")]
    public class HomeController : BaseController
    {

        [HttpGet("/")]
        public IActionResult Index(int category)
        {
            List<Product> products = new List<Product>();
            if (category > 0)
            {
                products = _context.Products.Where(x => x.CategoryId == category).ToList();
            }
            else
            {
                products = _context.Products.ToList();
            }

            SetProductIsFavorite(products);

            List<Category> categories = _context.Categories.ToList();
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Products = products;
            productViewModel.Categories = categories;
            return View(productViewModel);
        }


        private void SetProductIsFavorite(List<Product> products)
        {
            var user = GetUserBySession();
            if (user != null)
            {
                List<Favorite> favorites = _context.Favorites.Where(x => x.UserId == user.Id).ToList();

                foreach (var product in products)
                {
                    var favPro = favorites.Where(x => x.ProductId == product.Id);
                    if (favPro.Any())
                    {
                        product.IsFavorite = true;
                    }
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
