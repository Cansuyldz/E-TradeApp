using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;
using Newtonsoft.Json;

namespace MVC_E_ticaretWeb.Controllers
{
    //[Route("[controller]")]
    public class BaseController : Controller
    {
        public DataBaseContext _context = new();
        public User GetUserBySession()
        {
            User currentUser = null;
            if (HttpContext.Session.GetString("user") != null)
            {
                currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            }

            return currentUser;
        }

        public Cart GetCurrentUserCart()
        {
            User currentUser = GetUserBySession();
            if (currentUser == null) return null;
            return _context.Cart.Where(x => x.UserId == currentUser.Id).Include(x => x.CartProducts)
                .ThenInclude(x => x.Product).FirstOrDefault();
        }
    }
}
