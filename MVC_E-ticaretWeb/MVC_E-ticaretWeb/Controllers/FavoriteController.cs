using Microsoft.AspNetCore.Mvc;
using MVC_E_ticaretWeb.Models;
using MVC_E_ticaretWeb.ViewModels;

namespace MVC_E_ticaretWeb.Controllers
{
    [Route("/favorite")]
    public class FavoriteController : BaseController
    {
        DataBaseContext _context = new();

        

        [HttpPost("AddOrRemoveFavorite")]
        public FavoriteResponseViewModel AddOrRemoveFavorite(int productId, bool isAdded)
        {
            FavoriteResponseViewModel response = new();
            var user = GetUserBySession();
            if (user == null)
            {
                response.Message = "Kullanıcı bulunamadı !";
                return response;
            }

            var isHaveFavori = _context.Favorites.Where(f => f.UserId == user.Id && f.ProductId == productId).FirstOrDefault();
            if (isHaveFavori == null && !isAdded)
            {
                _context.Favorites.Add(new Models.Favorite()
                {
                    ProductId = productId,
                    UserId = user.Id
                });
                _context.SaveChanges();

                response.Message = "Ürün Favorilerinize eklendi.";
                response.Success = true;
                return response;
            }

            if (isHaveFavori != null && isAdded)
            {
                _context.Favorites.Remove(isHaveFavori);
                _context.SaveChanges();

                response.Message = "Ürün Favorilerinizden çıkarıldı.";
                response.Success = true;
                return response;
            }

            return null;
        }

    }
}

