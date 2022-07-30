using EdPlatform.Business;
using EdPlatform.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryBL categoryBL;
        public CategoriesController()
        {
            categoryBL = new CategoryBL();
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
            await categoryBL.AddCategory(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
