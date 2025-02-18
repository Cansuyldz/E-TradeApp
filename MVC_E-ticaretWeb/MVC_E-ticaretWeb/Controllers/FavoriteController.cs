using Microsoft.AspNetCore.Mvc;

namespace MVC_E_ticaretWeb.Controllers
{
    public class FavoriteController : Controller
    {
        public IActionResult Favorite()
        {
            return View();
        }
    }
}
