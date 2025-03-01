using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/account")]
    public class AccountController : BaseController
    {

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewBag.isHeaderShow = false;
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(User u)
        {
            ViewBag.isHeaderShow = false;
            User user = _context.Users.Where(x => x.Mail == u.Mail && x.Password == u.Password).FirstOrDefault();

            if (user != null)
            {
                var usera = new GenericPrincipal(new ClaimsIdentity(user.Mail), null);
                HttpContext.User = usera;

                string userString = JsonConvert.SerializeObject(user);
                byte[] userStringToByte = Encoding.UTF8.GetBytes(userString);
                byte[] userMailByte = Encoding.UTF8.GetBytes(user.Mail);
                HttpContext.Session.Set("user", userStringToByte);
                HttpContext.Session.Set("mail", userMailByte);
                
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = "Email veya şifre hatalı.";
                return View(ViewBag);
            }
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register")]
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

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Redirect("/");
        }
    }
}