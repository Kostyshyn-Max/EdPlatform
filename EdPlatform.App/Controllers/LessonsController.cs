using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Models;
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
        private readonly ICourseUserService _courseUserService;
        private readonly IAttemptService _attemptService;
        private readonly ICodeExerciseService _codeExerciseService;
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<LessonsController> _logger;
        public LessonsController(
            ILessonService lessonService, 
            IAuthorizationService authorizationService,
            ICourseUserService courseUserService,
            IAttemptService attemptService,
            ICodeExerciseService codeExerciseService,
            IExerciseService exerciseService,
            ILogger<LessonsController> ilogger)
        {
            _lessonService = lessonService;
            _authorizationService = authorizationService;
            _courseUserService = courseUserService;
            _attemptService = attemptService;
            _codeExerciseService = codeExerciseService;
            _exerciseService = exerciseService;
            _logger = ilogger;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId)
        {
            var course = await _lessonService.GetCourseById(courseId);
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

            return RedirectToAction("Edit", "Modules", new {courseId = courseId, moduleId = moduleId});
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
            else
                return RedirectToAction("Details", "Courses", new { courseId = courseId });
        }
    }
}
