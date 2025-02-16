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
        public IActionResult Login(User u)
        {
            var user = _context.Users
                 .FirstOrDefault(x => x.Mail == u.Mail && x.Password == u.Password);

            if (user != null)
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
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
    }
}