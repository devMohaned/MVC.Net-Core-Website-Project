using Final_Project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{

    [Authorize(Roles = "Admin,Moderator,Owner")]
    public class AdminController : Controller
    {
        private readonly IQuestionService _questionsService;
        private readonly IBrandService _brandService;


        public AdminController(IQuestionService questionsService, IBrandService brandService)
        {
            _questionsService = questionsService;
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.brandsCount = _brandService.GetAll().Count();
            adminViewModel.questionsCount = _questionsService.GetAll().Count();
            return View(adminViewModel);
        }

    }
}
