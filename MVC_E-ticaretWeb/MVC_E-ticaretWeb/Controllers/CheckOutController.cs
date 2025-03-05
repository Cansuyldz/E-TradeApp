using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/checkout")]
    public class CheckOutController : Controller
    {
        [HttpGet("/checkout/delivery")]
        public IActionResult Delivery()
        {
            return View();
        }
        [HttpGet("/checkout/payment")]
        public IActionResult Payment()
        {
            return View();
        }
    }
}
