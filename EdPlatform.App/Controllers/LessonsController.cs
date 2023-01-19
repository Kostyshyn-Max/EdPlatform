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
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        private readonly ICompletedLessonsViewService _completedLessonsViewService;
        private readonly ILogger<LessonsController> _logger;
        public LessonsController(
            ILessonService lessonService,
            ICourseUserService courseUserService,
            IAttemptService attemptService,
            ICodeExerciseService codeExerciseService,
            IExerciseService exerciseService,
            ICustomAuthorizationViewService customAuthorizattionViewService,
            ILogger<LessonsController> ilogger,
            ICompletedLessonsViewService completedLessonsViewService)
        {
            _lessonService = lessonService;
            _courseUserService = courseUserService;
            _attemptService = attemptService;
            _codeExerciseService = codeExerciseService;
            _exerciseService = exerciseService;
            _customAuthorizationViewService = customAuthorizattionViewService;
            _logger = ilogger;
            _completedLessonsViewService = completedLessonsViewService;
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.ModuleId = moduleId;

                return View(new LessonModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/Create")]
        public async Task<IActionResult> Create(int courseId, int moduleId, LessonModel lesson)
        {
            await _lessonService.CreateLesson(lesson);

            return RedirectToAction(nameof(ModulesController.Edit), nameof(ModulesController).Replace("Controller", ""), new { courseId = courseId, moduleId = moduleId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId)
        {
            var lesson = await _lessonService.Get(lessonId);

            if (await _customAuthorizationViewService.Authorize(User, lesson.Module.Course))
            {
                GetRedirectExercises(courseId, moduleId, lessonId, lesson);

                return View(lesson);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Edit")]
        public async Task<IActionResult> Edit(int courseId, int moduleId, int lessonId, LessonModel lesson)
        {
            await _lessonService.EditLesson(lesson);

            GetRedirectExercises(courseId, moduleId, lessonId, (await _lessonService.Get(lessonId)));

            return View(await _lessonService.Get(lessonId));
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Details")]
        public async Task<IActionResult> Details(int courseId, int moduleId, int lessonId)
        {
            var lesson = await _lessonService.Get(lessonId);
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                if (lesson.Exercises.Count() > 0)
                {
                    int notSolvedExerciseId = await _attemptService.GetNotSolvedExerciseId(lesson.Exercises.OrderBy(x => x.Order), courseUser.UserId);

                    var exercise = await _exerciseService.Get(notSolvedExerciseId);

                    ViewBag.NotSolvedExercise = new ExerciseRedirectViewModel()
                    {
                        Action = exercise.Discriminator + "Details",
                        CourseId = courseId,
                        ModuleId = moduleId,
                        LessonId = lessonId,
                        ExerciseId = notSolvedExerciseId
                    };
                }
                else
                {
                    ViewBag.NotSolvedExercise = null;
                }

                ViewBag.CompletedLessons = await _completedLessonsViewService.CreateListOfCompletedLessons(lesson.Module.Lessons.OrderBy(x => x.Order).ToList(), int.Parse(User.FindFirst("UserId").Value));

                return View(lesson);
            }

            return RedirectToAction(nameof(CoursesController.Details), nameof(CoursesController).Replace("Controller", ""), new { courseId = courseId });
        }

        private void GetRedirectExercises(int courseId, int moduleId, int lessonId, LessonModel lesson)
        {
            var redirectExercises = new List<ExerciseRedirectViewModel>();

            foreach (var exercise in lesson.Exercises)
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
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Delete")]
        public async Task<IActionResult> Delete(int courseId, int moduleId, int lessonId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                await _lessonService.Delete(lessonId);

                return RedirectToAction(nameof(ModulesController.Edit), nameof(ModulesController).Replace("Controller", ""),
                    new
                    {
                        courseId = courseId,
                        moduleId = moduleId,
                    }
                );
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
