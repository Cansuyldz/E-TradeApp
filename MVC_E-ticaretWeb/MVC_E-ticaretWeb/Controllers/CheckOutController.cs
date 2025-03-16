using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using MVC_E_ticaretWeb.Migrations;
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
                             Cart = cart,
                           Adresses = adresses
                    };

               return View(viewModel);
        }
    
        [HttpPost("Newaddress")]
        public AddressSuccessViewModel Newaddress(int UserId,string Name,string Surname, string Phone,string Streetaddress,string AddressLine, string Province, string District, string Neighbourhood, bool Success)
        {
            AddressSuccessViewModel response = new();
            var user = GetUserBySession();

            if (user == null)
            {
                response.Message = "Kullanıcı bulunamadı!";
                response.Success = false;
                return response;
            }

            try
            {
                var ishaveAddress = _context.Adresses.FirstOrDefault(a =>
                    a.UserId == user.Id &&
                    a.Name == Name &&
                    a.Surname == Surname &&
                    a.Phone == Phone &&
                    a.Streetaddress == Streetaddress &&
                    a.AddressLine == AddressLine &&
                    a.Province == Province &&
                    a.District == District &&
                    a.Neighbourhood == Neighbourhood);

                if (ishaveAddress == null)
                {
                    _context.Adresses.Add(new Models.Adress()
                    {
                        UserId = user.Id,
                        Name = Name,
                        Surname = Surname,
                        Phone = Phone,
                        Streetaddress = Streetaddress,
                        AddressLine = AddressLine,
                        Province = Province,
                        District = District,
                        Neighbourhood = Neighbourhood
                    });

                    _context.SaveChanges();
                    response.Message = "Adres başarıyla eklendi.";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Bu adres zaten eklenmiş.";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Adres eklenirken bir hata oluştu: " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        [HttpPost("NewCart")]
        public CartSuccessViewModel NewCart(int UserId, string NameonCard, string KartNumber,int Cvc, int Date,int Year, bool Success)
        {

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

            var cartProducts = _context.CartProducts
                                       .Where(cp => cp.CartId == cart.Id)
                                       .Include(cp => cp.Product)
                                       .ToList();

            var creditcards = _context.Creditcards
                                      .Where(c => c.UserId == user.Id)
                                      .ToList();

            CartAddressViewModel viewModel = new CartAddressViewModel
            {
                Cart = cart,
                Adresses = adresses,
                CartProducts = cartProducts,
                Creditcards = creditcards ?? new List<Creditcard>() 
            };

            return View(viewModel);
        }



    }
}
