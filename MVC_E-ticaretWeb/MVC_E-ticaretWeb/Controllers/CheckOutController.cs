using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/checkout")]
    public class CheckOutController : BaseController
    {

        [HttpGet("/checkout/delivery")]
        public IActionResult Delivery()
        {

            Cart cart = new();
            User user = GetUserBySession();
            if (user == null)
            {
                //return Redirect("/account/login");
            }
            else
            {
                cart = GetCurrentUserCart();
                return View(cart);
            }
            return View();
        }


        [HttpGet("/checkout/payment")]
        public IActionResult Payment()
        {
            Cart cart = GetCurrentUserCart();
            //if(cart.Address == null)
            //{
            //    return RedirectToAction("delivery");
            //}

            return View();
        }
    }
}
