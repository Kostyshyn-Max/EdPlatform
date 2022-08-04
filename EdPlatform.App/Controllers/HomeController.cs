using EdPlatform.App.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EdPlatform.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        public HomeController(ILogger<HomeController> logger, ICourseService courseService, ICategoryService categoryService)
        {
            _logger = logger;
            _courseService = courseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PopularCourses = (await _courseService.GetAll()).OrderByDescending(x => x.UsersJoined).Take(10).ToList();
            ViewBag.Categories = await _categoryService.GetAllCategories();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}