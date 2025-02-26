using Microsoft.AspNetCore.Mvc;

namespace MVC_E_ticaretWeb.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
