using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class IOCasesController : Controller
    {
        private readonly IIOCaseService _iocaseService;
        private readonly IAuthorizationService _authorizationService;
        public IOCasesController(IIOCaseService iocaseService, IAuthorizationService authorizationService)
        {
            _iocaseService = iocaseService;
            _authorizationService = authorizationService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, int lessonId, int codeExerciseId)
        {
            var course = await _iocaseService.GetCourseById(courseId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            if (authorizationResult.Succeeded)
            {
                ViewBag.ExerciseId = codeExerciseId;

                return View(new IOCaseModel());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, int lessonId, int codeExerciseId, IOCaseModel iOCase)
        {
            await _iocaseService.Create(iOCase);

            return RedirectToAction("CodeExerciseEdit", "Exercises", new { courseId = courseId, moduleId = moduleId, lessonId = lessonId, exerciseId = codeExerciseId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/{iOCaseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int codeExerciseId, int iOCaseId)
        {
            var course = await _iocaseService.GetCourseById(courseId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            if (authorizationResult.Succeeded)
            {
                var iOCase = await _iocaseService.GetById(iOCaseId);

                return View(iOCase);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/{iOCaseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int codeExerciseId, int iOCaseId, IOCaseModel iOCase)
        {
            await _iocaseService.Edit(iOCase);

            return View(iOCase);
        }
    }
}
