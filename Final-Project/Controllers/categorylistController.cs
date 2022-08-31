using Final_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final_Project.Controllers
{
    
    public class categorylistController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdService _adService;
        private readonly IBrandService BrandService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favoriteService;
        public categorylistController(ILogger<HomeController> logger, IUserService userService, IAdService adService, ICategoryService categoryService, IBrandService brandService, UserManager<User> userManager = null, IFavoriteService favoriteService = null)
        {
            _logger = logger;
            _adService = adService;
            _categoryService = categoryService;
            BrandService = brandService;
            _userManager = userManager;
            _favoriteService = favoriteService;
        }
        //public IActionResult Index(int id,string name)
        //{

        //    List<Ad> adList = _adService.GetAll().Where(a => a.CategoryId == id  ).ToList();
        //    int adCount = adList.Count;
        //    int Allpages = 0;
        //    if (adCount % 10 > 0)
        //    {
        //        Allpages /= 10;
        //        Allpages++;
        //    }
        //    else
        //    {
        //        Allpages /= 10;
        //    }
        //    Allpages = Allpages < 0 ? 1 : Allpages;
        //    ViewBag.Allpages = Allpages;
        //    List<Brand> brandList = BrandService.GetCategoryBrands(id).ToList();
        //    ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName");
        //    ViewBag.categoryid = id;
        //    ViewBag.categoryname = name;
        //    return View(adList);
        //}


        [HttpPost]
        public IActionResult Filter(int BrandId,float p1,float p2,int categoryid,string categoryname)
        {
       
            List<Ad> adList = _adService.GetAll().Where(a => a.CategoryId == categoryid && a.BrandId == BrandId && a.AdPrice <= p2 && a.AdPrice >= p1 && a.ProductReviewed == true).ToList();
            int adCount = adList.Count;
            List<Brand> brandList = BrandService.GetCategoryBrands(categoryid).ToList();
            ViewBag.categoryname = categoryname;
            @ViewBag.categoryid = categoryid;
            return View("Index", adList);

        }

        // Afif's Index
        /*  public IActionResult Index(int id, string name, int? BrandId, float? p1, float? p2,int page=0)
          {
              List<Ad> adList;
              List<Brand> brandList = BrandService.GetCategoryBrands(id).ToList();
              if (BrandId.HasValue)
              {
                  adList = _adService.GetAll().Where(a => a.CategoryId == id && a.BrandId == BrandId && a.AdPrice <= p2 && a.AdPrice >= p1).ToList();
                  ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName", BrandId);
              }
              else
              {
                  adList = _adService.GetAll().Where(a => a.CategoryId == id).ToList();
                  ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName");
              }
              int adCount = adList.Count;//start pagination
              int Allpages = 0;
              if (adCount % 10 > 0)
              {
                  Allpages /= 10;
                  Allpages++;
              }
              else
              {
                  Allpages /= 10;
              }
              Allpages = Allpages < 0 ? 1 : Allpages;
              adList= adList.Skip(page * 10).Take(10).ToList();//end pagination
              ViewBag.Allpages = Allpages;//end pagination
              ViewBag.categoryid = id;
              ViewBag.categoryname = name;
              return View(adList);
          }*/

        // Mohaned's Index
        public IActionResult Index(int id, string name, int? BrandId, float? p1, float? p2, string getAllBrands = "off" , int page = 1)
        {
           
            try
            {
                if (p1.ToString().Length == 0 || p1 == null)
                {
                    p1 = 0;
                }
            }
            catch (Exception ex)
            {
                p1 = 0;

            }

            try
            {
                if (p2.ToString().Length == 0 || p2 == null)
                {
                    p2 = 100000000;
                }
            }
            catch (Exception ex)
            {
                p2 = 100000000;

            }


            List<Ad> adList;
            List<Brand> brandList = BrandService.GetCategoryBrands(id).ToList();
            if (getAllBrands.ToLower().Equals("on"))
            {
                adList = _adService.GetAll().Where(a => a.CategoryId == id && a.AdPrice <= p2 && a.AdPrice >= p1 && a.ProductReviewed == true).ToList();
                ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName");
            }
            else if(BrandId.HasValue && !getAllBrands.ToLower().Equals("on") )
            {
                adList = _adService.GetAll().Where(a => a.CategoryId == id && a.BrandId == BrandId && a.AdPrice <= p2 && a.AdPrice >= p1 && a.ProductReviewed == true).ToList();
                ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName", BrandId);
            }
            else {
                adList = _adService.GetAll().Where(a => a.CategoryId == id && a.ProductReviewed == true).ToList();
                ViewBag.brandlist = new SelectList(brandList, "BrandId", "BrandName");
            }
          



            int adCount = adList.Count();//start pagination
            float endOfPages = adCount / 10f;
            int roundedEndOfPages =(int) Math.Ceiling(endOfPages);
            adList = adList.Skip((page - 1) * 10).Take(10).ToList();
            ViewBag.BrandId = BrandId;
            ViewBag.p1 = p1;
            ViewBag.p2 = p2;
            ViewBag.getAllBrands = getAllBrands;

            ViewBag.currentPage = page;
            ViewBag.Allpages = roundedEndOfPages;//end pagination
            ViewBag.categoryid = id;
            ViewBag.categoryname = name;
            if (User.Identity.IsAuthenticated)
            {
                string userid = _userManager.GetUserId(User);
                ViewBag.favs = _favoriteService.GetUserFavorites(userid).Select(x=>x.AdtId).ToList();
            }
            else
            {
                ViewBag.favs = null;
            }
            return View(adList);

        }


    }
}
