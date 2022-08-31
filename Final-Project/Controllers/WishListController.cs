using Final_Project.Models;
using Final_Project.Services.FavoriteServiceFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class WishListController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<User> _userManager;

        public WishListController(IFavoriteService favoriteService, UserManager<User> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index(int page = 1)
        {
            string userid = _userManager.GetUserId(User);
            List<Favorite> favoriteList = _favoriteService.GetUserFavorites(userid);
            int adCount = favoriteList.Count();//start pagination
            float endOfPages = adCount / 10f;
            int roundedEndOfPages = (int)Math.Ceiling(endOfPages);
            favoriteList = favoriteList.Skip((page - 1) * 10).Take(10).ToList();

            ViewBag.currentPage = page;
            ViewBag.Allpages = roundedEndOfPages;//end pagination
            return View(favoriteList);
        }
        [Authorize]
        public IActionResult RemoveFromFavourit(int id, int page = 1)
        {
            string userid = _userManager.GetUserId(User);
            _favoriteService.Delete(userid, id);

            return RedirectToAction("Index", new { page = page });
        }
    }
}
