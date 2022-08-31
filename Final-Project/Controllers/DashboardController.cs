using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAdService _adService;
        private readonly UserManager<User> _userManager;

        public DashboardController(UserManager<User> userManager, IAdService adService)
        {
            _userManager = userManager;
            _adService = adService;
        }

        public IActionResult Index(int page = 1)
        {
            String userId = _userManager.GetUserId(User);
            List<Ad> adList = _adService.GetUserAd(userId);
            int adCount = adList.Count();//start pagination
            float endOfPages = adCount / 10f;
            int roundedEndOfPages = (int)Math.Ceiling(endOfPages);
            adList = adList.Skip((page - 1) * 10).Take(10).ToList();

            ViewBag.currentPage = page;
            ViewBag.Allpages = roundedEndOfPages;//end pagination

            return View(adList);
        }


        public IActionResult preview(int adId)
        {
            Ad foundAd = _adService.Find(adId);
            foundAd.ProductReviewed = !foundAd.ProductReviewed;
            _adService.Update(foundAd);
            return Redirect("~/dashboard");
        }

        public IActionResult Deactivate(int adId, int page)
        {
            Ad foundAd = _adService.Find(adId);
            foundAd.ProductReviewed = !foundAd.ProductReviewed;
            _adService.Update(foundAd);
            return Redirect("~/dashboard?page=" + page);
        }
    }
}
