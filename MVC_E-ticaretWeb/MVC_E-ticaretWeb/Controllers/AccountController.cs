using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("Account")]
    public class AccountController : BaseController
    {
        [HttpGet("/Account/Orders")]
        public IActionResult Orders()
        {
            User user = GetUserBySession();
            List<Order> orders = _context.Orders.Where(x => x.UserId == user.Id).ToList();
            return View(orders);
        }
    }
}
