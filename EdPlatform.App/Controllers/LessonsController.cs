using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Models;
using EdPlatform.App.Services;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ICourseUserService _courseUserService;
        private readonly IAttemptService _attemptService;
        private readonly ICodeExerciseService _codeExerciseService;
        private readonly IExerciseService _exerciseService;
        private readonly ICustomAuthorizationViewService _customAuthorizattionViewService;
        private readonly ILogger<LessonsController> _logger;
        public LessonsController(
            ILessonService lessonService, 
            ICourseUserService courseUserService,
            IAttemptService attemptService,
            ICodeExerciseService codeExerciseService,
            IExerciseService exerciseService,
            ICustomAuthorizationViewService customAuthorizattionViewService,
            ILogger<LessonsController> ilogger)
        {
            _lessonService = lessonService;
            _courseUserService = courseUserService;
            _attemptService = attemptService;
            _codeExerciseService = codeExerciseService;
            _exerciseService = exerciseService;
            _customAuthorizattionViewService = customAuthorizattionViewService;
            _logger = ilogger;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId)
        {
            if (await _customAuthorizattionViewService.Authorize(User, courseId))
            {
                ViewBag.ModuleId = moduleId;

                return View(new LessonModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", "");
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, LessonModel lesson)
        {
            await _lessonService.CreateLesson(lesson);

            return RedirectToAction(nameof(ModulesController.Edit), nameof(ModulesController).Replace("Controller", ""), new {courseId = courseId, moduleId = moduleId});
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId)
        {
            var lesson = await _lessonService.Get(lessonId);

            if (await _customAuthorizattionViewService.Authorize(User, lesson.Module.Course))
            {
                var redirectExercises = new List<ExerciseRedirectViewModel>();

                foreach(var exercise in lesson.Exercises)
                {
                    redirectExercises.Add(new ExerciseRedirectViewModel()
                    {
                        Action = exercise.Discriminator + "Edit",
                        CourseId = courseId,
                        ModuleId = moduleId,
                        LessonId = lessonId,
                        ExerciseId = exercise.ExerciseId
                    });
                }

                ViewBag.RedirectExercises = redirectExercises;

                return View(lesson);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, LessonModel lesson)
        {
            await _lessonService.EditLesson(lesson); 
            
            return View(await _lessonService.Get(lessonId));
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Details")]
        public async Task<IActionResult> Details(int courseId, int moduleId, int lessonId)
        {
            var lesson = await _lessonService.Get(lessonId);
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                int notSolvedExerciseId = await _attemptService.GetNotSolvedExercise(lesson.Exercises.OrderBy(x => x.Order), courseUser.UserId);

                var exercise = await _exerciseService.Get(notSolvedExerciseId);

                ViewBag.NotSolvedExercise = new ExerciseRedirectViewModel() 
                { 
                    Action = exercise.Discriminator + "Details", 
                    CourseId = courseId,
                    ModuleId = moduleId, 
                    LessonId = lessonId, 
                    ExerciseId = notSolvedExerciseId
                };

                return View(lesson);
            }

            return RedirectToAction(nameof(CoursesController.Details), nameof(CoursesController).Replace("Controller", ""), new { courseId = courseId });
        }
    }
}
