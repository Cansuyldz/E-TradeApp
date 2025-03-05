using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/cart")]
    public class CartController : BaseController
    {
        DataBaseContext _context = new();

        [HttpGet("/cart")]
        public IActionResult Cart()
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


        [HttpPost("/Cart/RemoveProduct")]
        public bool RemoveProduct(int productId)
        {
            User currentUser = GetUserBySession();
            if (currentUser == null)
            {
                return false;
            }

            CartProduct removeCartProduct = _context.Cart.Where(x => x.UserId == currentUser.Id)
                .Include(x => x.CartProducts).FirstOrDefault()
                .CartProducts.Where(x => x.ProductId == productId).FirstOrDefault();
            _context.CartProducts.Remove(removeCartProduct);
            _context.SaveChanges();

            return true;
        }
        [HttpPost("/Cart/RemoveFromProduct")] 
        public bool RemoveFromProduct(int productId)
        {
            User currentUser = GetUserBySession();
            if (currentUser == null)
            {
                return false;
            }

            Cart cart = _context.Cart
                .Where(x => x.UserId == currentUser.Id)
                .Include(x => x.CartProducts)
                .FirstOrDefault();

            if (cart == null)
            {
                return false; // Kullanıcının sepeti yoksa işlem yapma
            }

            var cartProduct = cart.CartProducts?.FirstOrDefault(x => x.ProductId == productId);
            if (cartProduct == null)
            {
                return false; // Sepette bu ürün yoksa işlem yapma
            }

            if (cartProduct.Quantity > 1)
            {
                // Eğer adet 1'den büyükse sadece 1 azalt
                cartProduct.Quantity -= 1;
                cartProduct.TotalPrice = cartProduct.BasePrice * cartProduct.Quantity;
            }
            else
            {
                // Eğer ürünün adedi 1 ise, ürünü tamamen kaldır
                cart.CartProducts.Remove(cartProduct);
            }

            _context.SaveChanges();
            return true;
        }

        [HttpPost("/Cart/AddToCart")]
        public bool AddProduct(int productId)
        {
            User currentUser = GetUserBySession();
            if (currentUser == null)
            {
                return false;
            }

            Cart cart = _context.Cart.Where(x => x.UserId == currentUser.Id).Include(x => x.CartProducts).FirstOrDefault();
            var liveProduct = _context.Products.Where(x => x.Id == productId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart();
                cart.UserId = currentUser.Id;
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }

            CartProduct cartProduct = new CartProduct()
            {
                CartId = cart.Id,
                ProductId = productId,
                BasePrice = liveProduct.Price,
                Quantity = 1,
                TotalPrice = liveProduct.Price
            };

            if (cart.CartProducts == null)
            {
                cart.CartProducts = new List<CartProduct>();
                cart.CartProducts.Add(cartProduct);
                _context.SaveChanges();
            }
            else
            {
                var addedProduct = cart.CartProducts.Where(x => x.ProductId == productId);
                if (addedProduct.Any())
                {
                    addedProduct.FirstOrDefault().Quantity += 1;
                    addedProduct.FirstOrDefault().BasePrice = liveProduct.Price;
                    addedProduct.FirstOrDefault().TotalPrice = liveProduct.Price * addedProduct.FirstOrDefault().Quantity;
                }
                else
                {
                    cart.CartProducts.Add(cartProduct);
                }
            }

            _context.SaveChanges();


            return true;

        }
        [HttpGet("/Cart/GetCartItemCount")]
        public JsonResult GetCartItemCount()
        {
            User currentUser = GetUserBySession();

            if (currentUser == null)
            {
                return Json(0); // Kullanıcı giriş yapmamışsa 0 döndür
            }

            var cart = _context.Cart
                .Where(x => x.UserId == currentUser.Id)
                .Include(x => x.CartProducts)
                .FirstOrDefault();

            int itemCount = cart?.CartProducts?.Sum(x => x.Quantity) ?? 0;

            return Json(itemCount);
        }





    }
}
