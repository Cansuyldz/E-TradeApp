using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_E_ticaretWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public DataBaseContext _context = new();
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                // Resim uzantı kontrolü
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(Image.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("Image", "Sadece JPG, JPEG, PNG ve GIF dosyaları yüklenebilir.");
                    return View(product);
                }

                // Maksimum boyut kontrolü (5MB)
                if (Image.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "Maksimum 5MB boyutunda bir resim yükleyebilirsiniz.");
                    return View(product);
                }

                // wwwroot/images klasörüne kayıt işlemi
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Veritabanına kaydedilecek yol
                product.Image = $"images/{uniqueFileName}";


                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
          
            
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile Image)
        {
           
                if (Image != null && Image.Length > 0)
                {
                    // Resim uzantı kontrolü
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(Image.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Image", "Sadece JPG, JPEG, PNG ve GIF dosyaları yüklenebilir.");
                        return View(product);
                    }

                    // Maksimum boyut kontrolü (5MB)
                    if (Image.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Image", "Maksimum 5MB boyutunda bir resim yükleyebilirsiniz.");
                        return View(product);
                    }

                    // wwwroot/images klasörüne kayıt işlemi
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // Veritabanına kaydedilecek yol
                    product.Image = $"images/{uniqueFileName}";
                

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz kategori bulunamadı.";
                return RedirectToAction("Index");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
