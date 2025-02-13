using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    public class HomeController : Controller
    {
        DataBaseContext _context = new();
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
         List<Category> categories= _context.Categories.ToList();
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
