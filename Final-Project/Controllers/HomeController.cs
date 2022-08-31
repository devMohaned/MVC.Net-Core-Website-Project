using Final_Project.Services.UserServiceFolder;
using Final_Project.ViewModel;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IAdService _adService;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        public HomeController(ILogger<HomeController> logger, IUserService userService, IAdService adService, ICategoryService categoryService, IQuestionService questionService)

        {
            _logger = logger;
            _userService = userService;
            _adService = adService;
            _categoryService = categoryService;
            _questionService = questionService;
        }

        public IActionResult Index()
        {


            List<Ad> adList = _adService.GetRandomAds(5,4);
            List<Category> categoryList = new List<Category>();
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.adList = adList;
            homeViewModel.categoryList = categoryList;

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error404");
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult checkout()//we will recive Order Here with Order Details
        {
            ChekOutVM Model = new ChekOutVM();
            Model.user = _userService.Find(2); // we will get the user from identity
            return View(Model);//this line for passing model to View
        }
        [Route("About")]

        public IActionResult About()
        {
            return View();
        }
        [Route("Contact")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addQuestion(string Name, string Email, string? Phone, string? Subject, string Message)
        {
            Question question = new Question();
            question.Name = Name;
            question.Email = Email;
            question.Phone = Phone;
            question.Subject = Subject;
            question.Message = Message;
            _questionService.Create(question);
            return Redirect("~/contact");

        }


        [Route("FAQ")]
        public IActionResult FAQS()
        {
            return View();
        }


        //id,name to here 
        public IActionResult catlist(int cat,string catname)
        {
            return RedirectToAction("Index", "categorylist",new {id=cat,name=catname});
        }

        public IActionResult search( string str, int page = 1)
        {
            
            
        //    return View(_adService.searchforstr(str));





            List<Ad> adList = _adService.searchforstr(str);
            int adCount = adList.Count();//start pagination
            float endOfPages = adCount / 10f;
            int roundedEndOfPages = (int)Math.Ceiling(endOfPages);
            adList = adList.Skip((page - 1) * 10).Take(10).ToList();

            ViewBag.str = str;
            ViewBag.currentPage = page;
            ViewBag.Allpages = roundedEndOfPages;//end pagination
           
            return View(adList);
        }

    }
}