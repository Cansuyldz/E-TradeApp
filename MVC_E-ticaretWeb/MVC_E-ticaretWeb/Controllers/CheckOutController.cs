using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/checkout")]
    public class CheckOutController : BaseController
    {

        [HttpGet("/checkout/delivery")]
        public IActionResult Delivery()
        {

          User user = GetUserBySession();
    if (user == null)
    {
        return Redirect("/account/login");
    }

    Cart cart = GetCurrentUserCart();
    List<Adress> adresses = _context.Adresses
                                    .Where(a => a.UserId == user.Id)
                                    .ToList();

    CartAddressViewModel viewModel = new CartAddressViewModel
    {
        Cart = cart, // Burada liste değil, tek nesne olarak atıyoruz
        Adresses = adresses
    };

    return View(viewModel);
        }
        [HttpGet("Addressregistration")]
        public IActionResult Addressregistration()
        {
            return View();
        }
        [HttpPost("Addressregistration")]
        public IActionResult Addressregistration(Adress adress)
        {
            if (ModelState.IsValid)
            {
                _context.Adresses.Add(adress);
                _context.SaveChanges();
                return View();
            }
            return View(adress);
        }
        

        [HttpPost("/checkout/payment")]
        public IActionResult Payment()
        {
            User user = GetUserBySession();
            if (user == null)
            {
                return RedirectToAction("Delivery");
            }

            Cart cart = GetCurrentUserCart();
            List<Adress> adresses = _context.Adresses
                                            .Where(a => a.UserId == user.Id)
                                            .ToList();

            if (adresses == null || !adresses.Any())
            {
                return RedirectToAction("Delivery");
            }

            CartAddressViewModel viewModel = new CartAddressViewModel
            {
                Cart = cart,
                Adresses = adresses
            };

            return View(viewModel);
        }
    }
}
