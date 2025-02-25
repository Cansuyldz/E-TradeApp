using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            List<Category> categories = _context.Categories.ToList();
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Products = products;
            productViewModel.Categories = categories;

            return View(productViewModel);

        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
