using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            await _categoryService.AddCategory(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
