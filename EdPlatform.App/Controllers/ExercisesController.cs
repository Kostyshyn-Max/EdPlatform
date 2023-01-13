using EdPlatform.App.AuthorizationPolicy;
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
        private readonly ICourseUserService _courseUserService;
        private readonly IQuizService _quizService;
        private readonly ICheckQuizAnswerService _checkQuizAnswerService;
        public ExercisesController(
            ICodeExerciseService codeExerciseService,
            ILogger<ExercisesController> logger,
            ICodeExecutingService codeExecutingService,
            IIOCaseService iOCaseService,
            IAttemptService attemptService,
            IFillExerciseService fillExerciseService,
            ICheckFillExerciseAnswerService checkFillExerciseAnswerService,
            ICustomAuthorizationViewService customAuthorizationViewService,
            ICourseUserService courseUserService,
            IQuizService quizService,
            ICheckQuizAnswerService checkQuizAnswerService)
        {
            _codeExerciseService = codeExerciseService;
            _logger = logger;
            _codeExecutingService = codeExecutingService;
            _iOCaseService = iOCaseService;
            _attemptService = attemptService;
            _fillExerciseService = fillExerciseService;
            _checkFillExerciseAnswerService = checkFillExerciseAnswerService;
            _customAuthorizationViewService = customAuthorizationViewService;
            _courseUserService = courseUserService;
            _quizService = quizService;
            _checkQuizAnswerService = checkQuizAnswerService;
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
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                var exercise = await _codeExerciseService.GetById(exerciseId);
                ViewBag.Exercise = exercise;
                var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
                if (attempt != null)
                    attempt.UserAnswer = Regex.Escape(attempt.UserAnswer);
                ViewBag.Attempt = attempt;

                CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, exercise);
                ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(exercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

                return View(new CodeModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Code/{exerciseId}/Details")]
        public async Task<IActionResult> CodeExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId, CodeModel codeModel)
        {
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                IEnumerable<IOCaseModel>? iOCases = await _iOCaseService.GetFromExercise(exerciseId);
                codeModel.InputDatas = iOCases?.Select(x => x.InputData).ToList();
                codeModel.OutputDatas = iOCases.Select(x => x.OutputData).ToList();

                var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);

                List<bool> outputs = new List<bool>();


                if (attempt != null && !attempt.UserAnswer.Equals(codeModel.Code))
                    outputs = await _codeExecutingService.ExecuteCode(codeModel);
                
                if (attempt == null)
                    outputs = await _codeExecutingService.ExecuteCode(codeModel);


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
                ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(exercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

                return View();
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
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
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                var fillExercise = await _fillExerciseService.Get(exerciseId);
                ViewBag.Exercise = fillExercise;
                CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, fillExercise);

                var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
                ViewBag.Attempt = attempt;

                ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(fillExercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

                return View(new FillExerciseCheckModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Fill/{exerciseId}/Details")]
        public async Task<IActionResult> FillExerciseDetails(int courseId, int moduleId, int lessonId, int exerciseId, FillExerciseCheckModel fillExerciseCheckModel)
        {
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
                if (attempt == null)
                    await _checkFillExerciseAnswerService.ReviewUserAnswer(fillExerciseCheckModel);
                ViewBag.Attempt = attempt;

                var fillExercise = await _fillExerciseService.Get(exerciseId);
                ViewBag.Exercise = fillExercise;
                CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, fillExercise);

                ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(fillExercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

                return View(fillExerciseCheckModel);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
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

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Quiz")]
        public async Task<IActionResult> QuizCreate(int courseId, int moduleId, int lessonId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                ViewBag.LessonId = lessonId;

                return View(new QuizModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Create/Quiz")]
        public async Task<IActionResult> QuizCreate(int courseId, int moduleId, int lessonId, QuizModel quizExercise)
        {
            await _quizService.Create(quizExercise);
            
            return RedirectToAction(nameof(LessonsController.Edit), nameof(LessonsController).Replace("Controller", ""), new { courseId = courseId, moduleId = moduleId, lessonId = lessonId });
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Edit")]
        public async Task<IActionResult> QuizEdit(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                var quiz = await _quizService.Get(exerciseId);

                return View(quiz);
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Edit")]
        public async Task<IActionResult> QuizEdit(int courseId, int moduleId, int lessonId, int exerciseId, QuizModel quiz)
        {
            await _quizService.Edit(quiz);
            var updatedQuiz = await _quizService.Get(exerciseId);

            return View(updatedQuiz);
        }

        [HttpGet("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Details")]
        public async Task<IActionResult> QuizDetails(int courseId, int moduleId, int lessonId, int exerciseId)
        {
            var courseUser = await _courseUserService.Get(new CourseUserModel() { CourseId = courseId, UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0") });

            if (courseUser != null)
            {
                var exercise = await _quizService.Get(exerciseId);
                ViewBag.Exercise = exercise;

                var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
                if (attempt != null)
                    attempt.UserAnswer = Regex.Escape(attempt.UserAnswer);
                ViewBag.Attempt = attempt;

                CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, exercise);
                ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(exercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

                return View(new CaseViewModel());
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Modules/{moduleId}/Lessons/{lessonId}/Exercises/Quiz/{exerciseId}/Details")]
        public async Task<IActionResult> QuizDetails(int courseId, int moduleId, int lessonId, int exerciseId, CaseViewModel caseViewModel)
        {
            _logger.LogInformation(caseViewModel.CaseId.ToString());

            var attempt = await _attemptService.GetFromUserExercise(int.Parse(User.FindFirst("UserId").Value), exerciseId);
            if (attempt != null)
            {
                attempt.UserAnswer = Regex.Escape(attempt.UserAnswer);
            }
            else
            {
                await _checkQuizAnswerService.CheckAnswer(new QuizAnswerCheckModel()
                {
                    Result = caseViewModel.IsCorrect,
                    SelectedCaseId = caseViewModel.CaseId,
                    UserId = int.Parse(User.FindFirst("UserId").Value),
                    ExerciseId = exerciseId,
                });
            }

            var exercise = await _quizService.Get(exerciseId);
            ViewBag.Exercise = exercise;
            
            ViewBag.Attempt = attempt;

            CreateRedirectExercisesList("Details", courseId, moduleId, lessonId, exercise);
            ViewBag.Attempts = await _attemptService.GetAllAttemptsFromExercises(exercise.Lesson.Exercises, int.Parse(User.FindFirst("UserId").Value));

            return View(new CaseViewModel());
        }
    }
}