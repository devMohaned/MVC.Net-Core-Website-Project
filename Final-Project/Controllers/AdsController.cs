using Final_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Final_Project.Controllers
{
    public class AdsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly UserManager<User> _userManager;
        private readonly IAdService _adService;
        private readonly IFavoriteService _favoriteService;

        public AdsController(IAdService adService,
                             UserManager<User> userManager,
                             ICategoryService categoryService,
                             IBrandService brandService,
                             IFavoriteService favoriteService)
        {
            _adService = adService;
            _userManager = userManager;
            _categoryService = categoryService;
            _brandService = brandService;
            _favoriteService=favoriteService;
        }

        public IActionResult Index(int AdId)
        {
            int id = 0;
            ViewBag.next = 0;
            AdVm vm = new AdVm();
            if (AdId == null) { return BadRequest(); }

            if (User.Identity.IsAuthenticated)
            {
                string userid= _userManager.GetUserId(User);
               Favorite fav= _favoriteService.Find(userid, AdId);
                if (fav!=null)
                {
                    ViewBag.infav = true;
                }
                else
                {
                    ViewBag.infav = false;
                }
            }
            else
            {
                ViewBag.infav = false;
            }
            vm.ad = _adService.GetAd(AdId);
            if (vm.ad == null||vm.ad.ProductReviewed==false)
            {
                return View("Error404");
            }
            vm.images = _adService.GetAdImageList(AdId);
            vm.ads = _adService.getYouAlsoMayLike(vm.ad.CategoryId, vm.ad.AdId);

            foreach (var item in vm.ads)
            {
                if (item.AdId > ViewBag.next)
                {
                    ViewBag.next = item.AdId;
                }
            }
            _adService.AdViewd(AdId);
            return View(vm);
        }

        [Authorize]
        public IActionResult CreateAd()
        {
            ViewBag.request = "CreateAd";
            var categories = _categoryService.GetAll().Select(x => new {x.CategoryId,x.CategoryName});
            var brands = _brandService.GetCategoryBrands(categories.FirstOrDefault().CategoryId).Select(x => new { x.BrandId,x.BrandName});
            ViewBag.brands = new SelectList(brands, "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAd(Ad model, IFormFile? img1)
        {
            ViewBag.request = "CreateAd";
            model.SellerId = _userManager.GetUserId(User);
            model.PhoneRequested = 0;
            model.ProductReviewed = true;
            model.ViewCount = 0;
            model.AddedToFav = 0;
            model.SellerId = _userManager.GetUserId(User);
            _adService.Create(model);
            if (img1 != null)
            {
                if (img1.ContentType.ToLower().Contains("image"))
                {
                    //using (var ms = new MemoryStream())
                    //{
                    //    img1.CopyTo(ms);
                    //    var fileBytes = ms.ToArray();
                    //    model.AdMainImage = Convert.ToBase64String(fileBytes);
                    //}
                    string pathforfolder = "wwwroot/productimages/"+model.AdId;
                    Directory.CreateDirectory("./"+pathforfolder );
                    model.AdMainImage = "/productimages/" + model.AdId+"/" + img1.FileName;
                    using (var ms = new FileStream(pathforfolder + "/" + img1.FileName, FileMode.Create))
                    {
                        img1.CopyTo(ms);
                    }
                    _adService.Update(model);
                    return RedirectToAction("index", "home");
                }
            }
            if (model.AdId!=0)
            {
                return RedirectToAction("index", "home");
            }
            ModelState.AddModelError("", "Not Added");
            var categories = _categoryService.GetAll().Select(x => new { x.CategoryId, x.CategoryName });
            var brands = _brandService.GetCategoryBrands(categories.FirstOrDefault().CategoryId).Select(x => new { x.BrandId, x.BrandName });
            ViewBag.brands = new SelectList(brands, "BrandId", "BrandName", model.BrandId);
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", model.CategoryId);

            return View(model);

        }

        [Authorize]
        public IActionResult EditAd(int id)
        {
            
            ViewBag.request = "EditAd";
            Ad ad = _adService.Find(id);
            string userid = _userManager.GetUserId(User);
            if (ad.SellerId == userid)
            {
                List<Category> categories = _categoryService.GetAll();
                List<Brand> brands = _brandService.GetCategoryBrands(ad.CategoryId ?? 1);
                ViewBag.brands = new SelectList(brands, "BrandId", "BrandName", ad.BrandId);
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", ad.CategoryId);
                return View("CreateAd", ad);
            }
            return View("AccessDenied");

        }

        [HttpPost]
        [Authorize]
        public IActionResult EditAd(Ad model, IFormFile? img1)
        {
            ViewBag.request = "EditAd";
            string userid = _userManager.GetUserId(User);
            Ad ad = _adService.Find(model.AdId);
            if (model.SellerId == userid)
            {
                if (img1 != null)
                {
                    if (img1.ContentType.ToLower().Contains("image")&&img1.Length>0)
                    {
                        string pathforfolder = "wwwroot/productimages/" + model.AdId;
                      ad.AdMainImage=  model.AdMainImage = "/productimages/" + model.AdId + "/" + img1.FileName;
                        using (var ms = new FileStream(pathforfolder + "/" + img1.FileName, FileMode.Create))
                        {
                            img1.CopyTo(ms);
                        }

                    }
                }

                //model.AddedToFav = ad.AddedToFav;
                //model.ViewCount = ad.ViewCount;
                //model.PhoneRequested = ad.PhoneRequested;
                ad.AdTitle = model.AdTitle;
                ad.AdDescription = model.AdDescription;
                ad.AdBrief = model.AdBrief;
                ad.AdPrice = model.AdPrice;
                ad.CategoryId = model.CategoryId;
                ad.BrandId=model.BrandId;

                try
                {
                    _adService.Update(ad);
                }
                catch (Exception)
                {
                    List<Category> categories = _categoryService.GetAll();
                    List<Brand> brands = _brandService.GetCategoryBrands(model.CategoryId ?? 1);
                    ViewBag.brands = new SelectList(brands, "BrandId", "BrandName", model.BrandId);
                    ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", model.CategoryId);
                    return RedirectToAction("index", model);
                }
                return RedirectToAction("index", model);
                //return RedirectToAction("index", "home");
            }
            return View("AccessDenied");
        }

        public string AddToFavourit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "";
            }
            string userid = _userManager.GetUserId(User);
            Favorite fav= _favoriteService.Find(userid, id);
            if (fav!=null)
            {
                _favoriteService.Delete(userid, id);
                return "Removed";
            }
            else
            {
                _favoriteService.Create(new Favorite() { AdtId = id, CustomerId = userid });
                _adService.AdAddedToFavourit(id);
                return "Done";
            }
;
        }
        public void PhoneRequestedApi(int id)
        {
            _adService.AdPhoneRequested(id);
        }
        public string GetBrandforCategory(int id)
        {
            var model = _brandService.GetCategoryBrands(id).Select(x => new { x.BrandId, x.BrandName }).ToList();
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(model, options);

            //return View();
        }

        public IActionResult remove(int AdId)
        {
            _favoriteService.removeProductFavorites(AdId);
            _adService.Delete(AdId);
            return Redirect("~/");
        }
        public IActionResult Getimg(int id)
        {
           // var model= _adService.Find(id);
            return View();
        }
    }



}

