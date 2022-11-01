using EdPlatform.App.Services;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        private readonly ICaseService _caseService;
        public CasesController(ICustomAuthorizationViewService customAuthorizationViewService, ICaseService caseService)
        {
            _customAuthorizationViewService = customAuthorizationViewService;
            _caseService = caseService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Cases/Create")]
        public IActionResult Create(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            if (_customAuthorizationViewService.Authorize(User, courseId).Result)
            {
                ViewBag.ExerciseId = exerciseId;

                return View();
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Cases/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, int lessonId, int exerciseId, CaseModel quizCase)
        {
            await _caseService.Create(quizCase);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Cases/{caseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int exerciseId, int caseId)
        {
            if (_customAuthorizationViewService.Authorize(User, courseId).Result)
            {
                var quizCase = await _caseService.Get(caseId);

                return View(quizCase);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Cases/{caseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, int exerciseId, int caseId, CaseModel quizCase)
        {
            await _caseService.Edit(quizCase);

            var updatedCase = await _caseService.Get(caseId);

            return View(updatedCase);
        }
    }
}
