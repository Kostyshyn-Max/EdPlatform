﻿using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Models;
using EdPlatform.App.Services;
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
        private readonly ICodeExecutingService _codeExecutingService;
        private readonly IIOCaseService _iOCaseService;
        private readonly IAttemptService _attemptService;
        private readonly ILogger<ExercisesController> _logger;
        private readonly IFillExerciseService _fillExerciseService;
        private readonly ICheckFillExerciseAnswerService _checkFillExerciseAnswerService;
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        public ExercisesController(
            ICodeExerciseService codeExerciseService,
            ILogger<ExercisesController> logger,
            ICodeExecutingService codeExecutingService,
            IIOCaseService iOCaseService,   
            IAttemptService attemptService,
            IFillExerciseService fillExerciseService,
            ICheckFillExerciseAnswerService checkFillExerciseAnswerService,
            ICustomAuthorizationViewService customAuthorizationViewService)
        {
            _codeExerciseService = codeExerciseService;
            _logger = logger;
            _codeExecutingService = codeExecutingService;
            _iOCaseService = iOCaseService;
            _attemptService = attemptService;
            _fillExerciseService = fillExerciseService;
            _checkFillExerciseAnswerService = checkFillExerciseAnswerService;
            _customAuthorizationViewService = customAuthorizationViewService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Code")]
        public async Task<IActionResult> CodeExerciseCreate(int courseId, int moduleId, int lessonId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.LessonId = lessonId;

                return View(new CodeExerciseModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Code")]
        public async Task<IActionResult> CodeExerciseCreate(int courseId, int moduleId, int lessonId, CodeExerciseModel codeExercise)
        {
            ViewBag.LessonId = lessonId;

            await _codeExerciseService.Create(codeExercise);

            return RedirectToAction(nameof(LessonsController.Edit), nameof(LessonsController).Replace("Controller", ""), new { courseId = courseId, moduleId = moduleId, lessonId = lessonId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Edit")]
        public async Task<IActionResult> CodeExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                var codeExercise = await _codeExerciseService.GetById(exerciseId);

                return View(codeExercise);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
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
            var exercise = await _codeExerciseService.GetById(exerciseId);
            ViewBag.Exercise = exercise;
            var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
            if (attempt != null)
                attempt.UserAnswer = Regex.Escape(attempt.UserAnswer);
            ViewBag.Attempt = attempt;

            CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, exercise);

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

            var exercise = await _codeExerciseService.GetById(exerciseId);

            ViewBag.Exercise = exercise;
            newAttempt.UserAnswer = Regex.Escape(newAttempt.UserAnswer);
            ViewBag.Attempt = newAttempt;

            CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, exercise);

            return View();
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Fill")]
        public async Task<IActionResult> FillExerciseCreate(int courseId, int moduleId, int lessonId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.LessonId = lessonId;

                return View(new FillExerciseModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Fill")]
        public async Task<IActionResult> FillExerciseCreate(int courseId, int moduleId, int lessonId, FillExerciseModel fillExercise)
        {
            await _fillExerciseService.Create(fillExercise);

            return RedirectToAction(nameof(LessonsController.Edit), nameof(LessonsController).Replace("Controller", ""), new { courseId = courseId, moduleId = moduleId, lessonId = lessonId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Edit")]
        public async Task<IActionResult> FillExerciseEdit(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                var fillExercise = await _fillExerciseService.Get(exerciseId);

                return View(fillExercise);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
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
            CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, fillExercise);

            return View(new FillExerciseCheckModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Details")]
        public async Task<IActionResult> FillExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId, FillExerciseCheckModel fillExerciseCheckModel)
        {
            var fillExercise = await _fillExerciseService.Get(exerciseId);
            ViewBag.Exercise = fillExercise;
            CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, fillExercise);

            await _checkFillExerciseAnswerService.ReviewUserAnswer(fillExerciseCheckModel);

            return View(fillExerciseCheckModel);
        }

        private void CreateRedirectExercisesList(string action, int courseId, int moduleId, int lessonId, ExerciseModel fillExercise)
        {
            var redirectExercises = new List<ExerciseRedirectViewModel>();
            fillExercise.Lesson.Exercises = fillExercise.Lesson.Exercises.OrderBy(x => x.Order);

            foreach (var exercise in fillExercise.Lesson.Exercises)
            {
                redirectExercises.Add(new ExerciseRedirectViewModel()
                {
                    Action = exercise.Discriminator + action,
                    CourseId = courseId,
                    ModuleId = moduleId,
                    LessonId = lessonId,
                    ExerciseId = exercise.ExerciseId
                });
            }

            ViewBag.RedirectExercises = redirectExercises;
        }
    }
}
