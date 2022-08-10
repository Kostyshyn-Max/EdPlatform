﻿using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ICodeExerciseService _codeExerciseService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICodeExecutingService _codeExecutingService;
        private readonly IIOCaseService _iOCaseService;
        private readonly ILogger<ExercisesController> _logger;
        public ExercisesController(
            ICodeExerciseService codeExerciseService, 
            IAuthorizationService authorizationService,
            ILogger<ExercisesController> logger,
            ICodeExecutingService codeExecutingService,
            IIOCaseService iOCaseService)
        {
            _codeExerciseService = codeExerciseService;
            _authorizationService = authorizationService;
            _logger = logger;
            _codeExecutingService = codeExecutingService;
            _iOCaseService = iOCaseService;
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

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/Edit")]
        public async Task<IActionResult> CodeExerciseEdit(int courseId, int moduleId, int lessonId, int codeExerciseId)
        {
            var course = await _codeExerciseService.GetCourseById(courseId);
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());
            if (authrorizationResult.Succeeded)
            {
                var codeExercise = await _codeExerciseService.GetById(codeExerciseId);

                return View(codeExercise);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/Edit")]
        public async Task<IActionResult> CodeExerciseEdit(int courseId, int moduleId, int lessonId, int codeExerciseId, CodeExerciseModel codeExercise)
        {
            await _codeExerciseService.Edit(codeExercise);

            return View(codeExercise);
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/Details")]
        public async Task<IActionResult> CodeExerciseDetails(int courseId, int moduleId, int lessonId, int codeExerciseId)
        {
            return View(new CodeModel());
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{codeExerciseId}/Details")]
        public async Task<IActionResult> CodeExerciseDetails(int courseId, int moduleId, int lessonId, int codeExerciseId, CodeModel codeModel)
        {
            var iOCases = await _iOCaseService.GetFromExercise(codeExerciseId);
            codeModel.InputDatas = iOCases.Select(x => x.InputData).ToList();
            codeModel.OutputDatas = iOCases.Select(x => x.OutputData).ToList();

            var output = await _codeExecutingService.ExecuteCode(codeModel);
            foreach (var o in output)
            {
                _logger.LogInformation(o.ToString());
            }

            return View();
        }
    }
}
