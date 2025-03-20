using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MVC_E_ticaretWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "123456")
            {
                HttpContext.Session.SetString("AdminUser", username);
                return RedirectToAction("Index", "AdminPanel");
            }
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminUser");
            return RedirectToAction("Index");
        }
    }
}
