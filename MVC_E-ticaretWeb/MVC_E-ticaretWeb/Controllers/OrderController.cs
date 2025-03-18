using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("order")]
    public class OrderController : BaseController
    {
        [HttpPost("/order/placeorder")]
        public IActionResult PlaceOrder()
        {
            Cart cart = GetCurrentUserCart();

            Order newOrder = new Order()
            {
                UserId = cart.UserId,
                AddressId = cart.AddressId,
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            newOrder.OrderProducts = new List<OrderProducts>();

            foreach (var item in cart.CartProducts)
            {
                OrderProducts orderProduct = new OrderProducts()
                {
                    OrderId = newOrder.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    BasePrice = item.BasePrice,
                    TotalPrice = item.TotalPrice
                };
                newOrder.OrderProducts.Add(orderProduct);
            }
            _context.Orders.Update(newOrder);
            _context.SaveChanges();

            _context.Cart.Remove(cart);
            _context.SaveChanges();

            return Redirect("/order/confirmation");
        }
        
        [HttpGet("/order/confirmation")]
        public async Task<IActionResult> OrderConfirmation()
        {
            return View();
        }

    }
}
