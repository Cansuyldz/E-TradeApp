using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    public class AccountController : Controller
    {
        DataBaseContext _context = new();
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Guest g)
        {
            var guest = _context.Guests
                 .FirstOrDefault(x => x.Mail == g.Mail && x.Mail == g.Mail);

            if (guest != null)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = "Email veya şifre hatalı.";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Guests.Add(guest);
                _context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(guest);
        }
    }
}
