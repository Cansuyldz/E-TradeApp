using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Order> orders = _context.Orders
                .Include(x=> x.Address)
                .Include(x=> x.User)
                .Where(x => x.UserId == user.Id).ToList();
            return View(orders);
        }
    }
}
