using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;
using Newtonsoft.Json;

namespace MVC_E_ticaretWeb.Utils
{
    public class AppSessionFilter : IActionFilter
    {
        public DataBaseContext _context = new();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                User currentUser = null;

                if (context.HttpContext.Session.GetString("user") != null)
                {
                    currentUser = JsonConvert.DeserializeObject<User>(context.HttpContext.Session.GetString("user"));

                }
                if (currentUser != null)
                {
                    controller.ViewBag.cartItemsCount = _context.Cart
                                                        .Where(x => x.UserId == currentUser.Id)
                                                        .Include(x => x.CartProducts)
                                                        .ThenInclude(x => x.Product)
                                                        .Select(x => x.CartProducts.Sum(cp => cp.Quantity))
                                                        .FirstOrDefault();
                }
            }
        }
    }
}
