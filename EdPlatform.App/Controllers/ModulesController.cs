using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Services;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IModuleService _moduleService;
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        public ModulesController(IModuleService moduleService, ICustomAuthorizationViewService customAuthorizationViewService)
        {
            _moduleService = moduleService;
            _customAuthorizationViewService = customAuthorizationViewService;
        }

        [HttpGet("Courses/{courseId}/Modules/Create")]
        public async Task<IActionResult> Create(int courseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.CourseId = courseId;

                return View(new ModuleModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/Create")]
        public async Task<IActionResult> Create(int courseId, ModuleModel module)
        {
            await _moduleService.CreateModule(module);

            return RedirectToAction(nameof(CoursesController.Edit), nameof(CoursesController).Replace("Controller", ""), new {courseId = courseId});
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId)
        {
            var module = await _moduleService.GetById(moduleId);
            
            if (await _customAuthorizationViewService.Authorize(User, module.Course))
                return View(module);

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, ModuleModel module)
        {
            await _moduleService.EditModule(module);

            return View(await _moduleService.GetById(moduleId));
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Delete")]
        public async Task<IActionResult> Delete(int courseId, int moduleId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                await _moduleService.Delete(moduleId);

                return RedirectToAction(nameof(CoursesController.Edit), nameof(CoursesController).Replace("Controller", ""),
                    new
                    {
                        courseId = courseId,
                    }
                );
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
