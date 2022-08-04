using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IModuleService _moduleService;
        private readonly IAuthorizationService _authorizationService;
        public ModulesController(IModuleService moduleService, IAuthorizationService authorizationService)
        {
            _moduleService = moduleService;
            _authorizationService = authorizationService;
        }

        [HttpGet("Courses/{courseId}/Modules/Create")]
        public async Task<IActionResult> Create(int courseId)
        {
            var course = await _moduleService.GetCourseById(courseId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            if (authorizationResult.Succeeded)
            {
                ViewBag.CourseId = courseId;

                return View(new ModuleModel());
            }

            return RedirectToAction("Index", "Courses");
        }

        [HttpPost("Courses/{courseId}/Modules/Create")]
        public async Task<IActionResult> Create(int courseId, ModuleModel module)
        {
            await _moduleService.CreateModule(module);

            return RedirectToAction("Edit", "Courses", new {courseId = courseId});
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId)
        {
            var module = await _moduleService.GetById(moduleId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, module.Course, new EditCourseRequirement());

            if (authorizationResult.Succeeded)
            {
                return View(module);
            }
            else
            {
                return RedirectToAction("Index", "Courses");
            }
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, ModuleModel module)
        {
            await _moduleService.EditModule(module);

            return View(await _moduleService.GetById(moduleId));
        }
    }
}
