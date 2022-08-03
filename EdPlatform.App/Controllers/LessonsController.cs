using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public IActionResult Create(int courseId, int moduleId)
        {
            ViewBag.ModuleId = moduleId;

            return View(new LessonModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, LessonModel lesson)
        {
            await _lessonService.CreateLesson(lesson);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId)
        {
            var lesson = await _lessonService.Get(lessonId);

            return View(lesson);
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, LessonModel lesson)
        {
            await _lessonService.EditLesson(lesson); 
            
            return View(await _lessonService.Get(lessonId));
        }
    }
}
