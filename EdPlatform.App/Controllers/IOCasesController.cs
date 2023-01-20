using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Services;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class IOCasesController : Controller
    {
        private readonly IIOCaseService _iocaseService;
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        public IOCasesController(IIOCaseService iocaseService, ICustomAuthorizationViewService customAuthorizationViewService)
        {
            _iocaseService = iocaseService;
            _customAuthorizationViewService = customAuthorizationViewService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, int lessonId, int codeExerciseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.ExerciseId = codeExerciseId;

                return View(new IOCaseModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, int lessonId, int codeExerciseId, IOCaseModel iOCase)
        {
            await _iocaseService.Create(iOCase);

            return RedirectToAction(nameof(ExercisesController.CodeExerciseEdit), nameof(ExercisesController).Replace("Controller", ""), new { courseId = courseId, moduleId = moduleId, lessonId = lessonId, exerciseId = codeExerciseId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/{iOCaseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int codeExerciseId, int iOCaseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                var iOCase = await _iocaseService.GetById(iOCaseId);

                return View(iOCase);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/IOCases/{iOCaseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int codeExerciseId, int iOCaseId, IOCaseModel iOCase)
        {
            await _iocaseService.Edit(iOCase);

            return View(iOCase);
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/IOCases/{iOCaseId}/Delete")]
        public async Task<IActionResult> Delete(int courseId, int moduleId, int lessonId, int exerciseId, int iOCaseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                await _iocaseService.Delete(iOCaseId);

                return RedirectToAction(nameof(ExercisesController.CodeExerciseEdit), nameof(ExercisesController).Replace("Controller", ""),
                    new
                    {
                        courseId = courseId,
                        moduleId = moduleId,
                        lessonId = lessonId,
                        exerciseId = exerciseId
                    }
                );
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
