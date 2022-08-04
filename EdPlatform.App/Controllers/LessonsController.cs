using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IAuthorizationService _authorizationService;
        public LessonsController(ILessonService lessonService, IAuthorizationService authorizationService)
        {
            _lessonService = lessonService;
            _authorizationService = authorizationService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId)
        {
            var course = _lessonService.GetCourseById(courseId);
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            if (authrorizationResult.Succeeded)
            {
                ViewBag.ModuleId = moduleId;

                return View(new LessonModel());
            }
            else
            {
                return RedirectToAction("Index", "Courses");
            }
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

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, lesson.Module.Course, new EditCourseRequirement());
            if (authorizationResult.Succeeded)
            {
                return View(lesson);
            }
            else
            {
                return RedirectToAction("Index", "Courses");
            }
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, LessonModel lesson)
        {
            await _lessonService.EditLesson(lesson); 
            
            return View(await _lessonService.Get(lessonId));
        }
    }
}
