using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EdPlatform.App.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ICodeExerciseService _codeExerciseService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICodeExecutingService _codeExecutingService;
        private readonly IIOCaseService _iOCaseService;
        private readonly IAttemptService _attemptService;
        private readonly ILogger<ExercisesController> _logger;
        private readonly IFillExerciseService _fillExerciseService;
        public ExercisesController(
            ICodeExerciseService codeExerciseService,
            IAuthorizationService authorizationService,
            ILogger<ExercisesController> logger,
            ICodeExecutingService codeExecutingService,
            IIOCaseService iOCaseService,   
            IAttemptService attemptService,
            IFillExerciseService fillExerciseService)
        {
            _codeExerciseService = codeExerciseService;
            _authorizationService = authorizationService;
            _logger = logger;
            _codeExecutingService = codeExecutingService;
            _iOCaseService = iOCaseService;
            _attemptService = attemptService;
            _fillExerciseService = fillExerciseService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Code")]
        public async Task<IActionResult> CodeExerciseCreate(int courseId, int moduleId, int lessonId)
        {
            var course = await _codeExerciseService.GetCourseById(courseId);
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            if (authrorizationResult.Succeeded)
            {
                ViewBag.LessonId = lessonId;

                return View(new CodeExerciseModel());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Code")]
        public async Task<IActionResult> CodeExerciseCreate(int courseId, int moduleId, int lessonId, CodeExerciseModel codeExercise)
        {
            ViewBag.LessonId = lessonId;

            await _codeExerciseService.Create(codeExercise);

            return RedirectToAction("Edit", "Lessons", new { courseId = courseId, moduleId = moduleId, lessonId = lessonId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Edit")]
        public async Task<IActionResult> CodeExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            var course = await _codeExerciseService.GetCourseById(courseId);
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());
            if (authrorizationResult.Succeeded)
            {
                var codeExercise = await _codeExerciseService.GetById(exerciseId);

                return View(codeExercise);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Edit")]
        public async Task<IActionResult> CodeExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId, CodeExerciseModel codeExercise)
        {
            await _codeExerciseService.Edit(codeExercise);

            var updatedCodeExercise = await _codeExerciseService.GetById(exerciseId);

            return View(updatedCodeExercise);
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Details")]
        public async Task<IActionResult> CodeExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            ViewBag.Exercise = await _codeExerciseService.GetById(exerciseId);
            var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
            if (attempt != null)
                attempt.UserAnswer = Regex.Escape(attempt.UserAnswer);
            ViewBag.Attempt = attempt;

            return View(new CodeModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Details")]
        public async Task<IActionResult> CodeExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId, CodeModel codeModel)
        {
            IEnumerable<IOCaseModel>? iOCases = await _iOCaseService.GetFromExercise(exerciseId);
            codeModel.InputDatas = iOCases?.Select(x => x.InputData).ToList();
            codeModel.OutputDatas = iOCases.Select(x => x.OutputData).ToList();

            var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);

            List<bool> outputs = new List<bool>();

            if (attempt != null)
            {
                if (!attempt.UserAnswer.Equals(codeModel.Code))
                {
                    outputs = await _codeExecutingService.ExecuteCode(codeModel);
                }
            }

            var newAttempt = new AttemptModel
            {
                UserId = int.Parse(User.FindFirst("UserId").Value),
                ExerciseId = exerciseId,
                UserAnswer = codeModel.Code,
            };

            await _attemptService.Create(newAttempt, outputs);

            ViewBag.Exercise = await _codeExerciseService.GetById(exerciseId);
            newAttempt.UserAnswer = Regex.Escape(newAttempt.UserAnswer);
            ViewBag.Attempt = newAttempt;

            return View();
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Fill")]
        public IActionResult FillExerciseCreate(int courseId, int moduleId, int lessonId)
        {
            ViewBag.LessonId = lessonId;

            return View(new FillExerciseModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Fill")]
        public async Task<IActionResult> FillExerciseCreate(int courseId, int moduleId, int lessonId, FillExerciseModel fillExercise)
        {
            await _fillExerciseService.Create(fillExercise);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Edit")]
        public async Task<IActionResult> FillExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            var fillExercise = await _fillExerciseService.Get(exerciseId);

            return View(fillExercise);
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Edit")]
        public async Task<IActionResult> FillExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId, FillExerciseModel fillExercise)
        {
            await _fillExerciseService.Edit(fillExercise);

            var newFillExercise = await _fillExerciseService.Get(exerciseId);

            return View(newFillExercise);
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Details")]
        public async Task<IActionResult> FillExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            var fillExercise = await _fillExerciseService.Get(exerciseId);

            ViewBag.Exercise = fillExercise;

            return View(new FillExerciseCheckModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Details")]
        public async Task<IActionResult> FillExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId, FillExerciseCheckModel fillExerciseCheckModel)
        {
            var fillExercise = await _fillExerciseService.Get(exerciseId);

            ViewBag.FillExercise = fillExercise;

            return View();
        }
    }
}
