using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IModuleService _moduleService;
        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet("Courses/{courseId}/Modules/Create")]
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;

            return View(new ModuleModel());
        }

        [HttpPost("Courses/{courseId}/Modules/Create")]
        public async Task<IActionResult> Create(int courseId, ModuleModel module)
        {
            await _moduleService.CreateModule(module);

            return RedirectToAction("Edit", "Courses", courseId);
        }

        [HttpGet("Courses/{courseId}/Modules/Edit/{moduleId}")]
        public async Task<IActionResult> Edit(int courseId, int moduleId)
        {
            return View(await _moduleService.GetById(moduleId));
        }

        [HttpPost("Courses/{courseId}/Modules/Edit/{moduleId}")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, ModuleModel module)
        {
            await _moduleService.EditModule(module);

            return RedirectToAction("Edit", "Courses", new { id = courseId });
        }
    }
}
