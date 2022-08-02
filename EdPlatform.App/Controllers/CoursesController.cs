using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Models;
using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace EdPlatform.App.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _courseService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICategoryService _categoryService;
        private readonly IModuleService _moduleService;
        public CoursesController(ILogger<CoursesController> logger, ICourseService courseService, IAuthorizationService authorizationService, ICategoryService categoryService, IModuleService moduleService)
        {
            _logger = logger;
            _courseService = courseService;
            _categoryService = categoryService;
            _authorizationService = authorizationService;
            _moduleService = moduleService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = await _courseService.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CreateSelectListFromCategories();

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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var course = await _courseService.GetById(id);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());
            if (authorizationResult.Succeeded)
            {
                await CreateSelectListFromCategories();
                ViewBag.Modules = course.Modules;

                return View(course);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]CourseModel course)
        {
            course.AuthorId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            await _courseService.EditCourse(course);

            var updatedCourse = await _courseService.GetById(id);
            await CreateSelectListFromCategories();
            ViewBag.Modules = updatedCourse.Modules;
            return View(updatedCourse);
        }

        private async Task CreateSelectListFromCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            List<SelectListItem> selectCategories = categories.Select(c => new SelectListItem() { Value = c.CategoryId.ToString(), Text = c.CategoryName }).ToList();
            ViewBag.Categories = selectCategories;
        }
    }
}
