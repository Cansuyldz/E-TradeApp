using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;

namespace MVC_E_ticaretWeb.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Order()
        {
            var siparisler = new List<Order>
    {
        new Order
        {
            Tarih = new DateTime(2025, 1, 10, 21, 51, 0),
            Ozet = "1 Teslimat, 1 Ürün",
            Alici = "Hasan Aydemir",
            Tutar = 1199,
            Durum = "Teslim Edildi",
            TeslimTarihi = new DateTime(2025, 1, 13),
            ResimUrl = "~/Content/images/headset.png"
        },
        new Order
        {
            Tarih = new DateTime(2025, 1, 10, 13, 10, 0),
            Ozet = "1 Teslimat, 2 Ürün",
            Alici = "Hasan Fırat Aydemir",
            Tutar = 8366,
            Durum = "Teslim Edildi",
            TeslimTarihi = new DateTime(2025, 1, 13),
            ResimUrl = "~/Content/images/chair.png"
        }
    };

            if (siparisler.Count == 0)
            {
                ViewBag.Mesaj = "Henüz siparişiniz bulunmamaktadır. Alışverişe başlamak için buraya tıklayın.";
                return View("BosSiparis");
            }

            return View(siparisler);
        }

    }
}
