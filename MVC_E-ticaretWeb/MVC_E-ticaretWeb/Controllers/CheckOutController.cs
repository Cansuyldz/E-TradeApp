using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
