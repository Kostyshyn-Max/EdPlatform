using EdPlatform.App.Models;
using EdPlatform.Business;
using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EdPlatform.App.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly CategoryBL _categoryBL;
        private readonly ILogger<CoursesController> _logger;
        public CoursesController(ILogger<CoursesController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;

            _categoryBL = new();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = await _courseService.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryBL.GetAllCategories();

            List<SelectListItem> selectCategories = categories.Select(c => new SelectListItem() { Value = c.CategoryName, Text = c.CategoryName }).ToList();

            ViewBag.Categories = selectCategories;

            return View(new CourseModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseModel course)
        {
            var claims = User.Claims.ToList();
            if (claims != null)
            {
                string s = claims.SingleOrDefault(x => x.Type.Equals("UserId")).Value;
                var userId = int.Parse(s);
                course.AuthorId = userId;
            }

            await _courseService.CreateCourse(course);
            return RedirectToAction(nameof(Index));
        }
    }
}
