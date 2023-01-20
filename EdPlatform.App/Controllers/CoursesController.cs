using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.App.Models;
using EdPlatform.App.Services;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using EdPlatform.Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace EdPlatform.App.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IModuleService _moduleService;
        private readonly ICourseUserService _courseUserService;
        private readonly IImageService _imageService;
        private readonly ICustomAuthorizationViewService _customAuthorizationViewService;
        private readonly ICompletedLessonsViewService _completedLessonsViewService;
        private readonly ICommentService _commentService;

        public CoursesController(
            ILogger<CoursesController> logger, 
            ICourseService courseService,
            ICategoryService categoryService,
            IModuleService moduleService,
            ICourseUserService courseUserService,
            ICustomAuthorizationViewService customAuthorizationViewService,
            ICompletedLessonsViewService completedLessonsViewService,
            ICommentService commentService)
        {
            _logger = logger;
            _courseService = courseService;
            _categoryService = categoryService;
            _moduleService = moduleService;
            _courseUserService = courseUserService;
            _customAuthorizationViewService = customAuthorizationViewService;
            _completedLessonsViewService = completedLessonsViewService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var courseUsers = await _courseUserService.GetAllFromUser(userId);

            ViewBag.Courses = await _courseService.GetAll();
            ViewBag.CourseUsers = courseUsers;

            return View();
        }

        [HttpGet("Courses/{courseId}/Details")]
        public async Task<IActionResult> Details([FromRoute] int courseId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var comments = await _commentService.GetAllByCourseId(courseId);

            var courseUser = await _courseUserService.Get(new() { UserId = userId, CourseId = courseId});
            var course = await _courseService.GetById(courseId);
            
            ViewBag.Course = course;
            ViewBag.UserId = userId;
            ViewBag.CourseUser = courseUser; 
            ViewBag.CompletedLessons = await _completedLessonsViewService.CreateListOfCompletedLessons(course.Modules.SelectMany(x => x.Lessons).ToList(), int.Parse(User.FindFirst("UserId").Value));
            ViewBag.Comments = comments;

            return View(new CourseUserModel());
        }

        [HttpPost("Courses/{courseId}/Details")]
        public async Task<IActionResult> Details([FromRoute] int courseId, CourseUserModel courseUser)
        {
            if ((await _courseUserService.Get(courseUser)) == null)
                await _courseUserService.CreateCourseUser(courseUser);

            var comments = await _commentService.GetAllByCourseId(courseId);

            var course = await _courseService.GetById(courseId);
            ViewBag.Course = course;
            ViewBag.UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            ViewBag.CourseUser = courseUser;
            ViewBag.CompletedLessons = await _completedLessonsViewService.CreateListOfCompletedLessons(course.Modules.SelectMany(x => x.Lessons).OrderBy(x => x.Order).ToList(), int.Parse(User.FindFirst("UserId").Value));
            ViewBag.Comments = comments;

            return View(courseUser);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CreateSelectListFromCategories();

            return View(new CourseViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            string? sid = User.FindFirst("UserId")?.Value;
            if (sid == null || !int.TryParse(sid, out int id))
                return Redirect("/");

            course.AuthorId = id;

            if (course.Image == null)
            {
                ModelState.AddModelError("Image", "Course image must be not null");
                await CreateSelectListFromCategories();
                return View(course);
            }

            if (!ModelState.IsValid)
            {
                await CreateSelectListFromCategories();
                return View(course);
            }

            await _courseService.CreateCourse(new()
            {
                AuthorId = course.AuthorId,
                CategoryId = course.CategoryId,
                ContentType = course.Image.ContentType,
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Description = course.Description,
                Image = await GetBytes(course.Image),
                ImageName = course.ImageName,
                Modules = course.Modules,
                UsersJoined = course.UsersJoined,
                ShortDescription = course.ShortDescription
            });

            return RedirectToAction(nameof(Index));
        }
        public static async Task<byte[]> GetBytes(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        [HttpGet("Courses/{courseId}/Edit")]
        public async Task<IActionResult> Edit(int courseId)
        {
            var course = await _courseService.GetById(courseId);

            if (await _customAuthorizationViewService.Authorize(User, course))
            {
                await CreateSelectListFromCategories();

                return View(new CourseViewModel()
                {
                    AuthorId = course.AuthorId,
                    CategoryId = course.CategoryId,
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    ImageName = course.ImageName,
                    Modules = course.Modules,
                    UsersJoined = course.UsersJoined
                });
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpPost("Courses/{courseId}/Edit")]
        public async Task<IActionResult> Edit([FromRoute]int courseId, [FromForm]CourseViewModel course)
        {
            course.AuthorId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
           
            await _courseService.EditCourse(new()
            {
                AuthorId = course.AuthorId,
                CategoryId = course.CategoryId,
                ContentType = course.Image?.ContentType,
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Description = course.Description,
                Image = (course.Image!=null)?await GetBytes(course.Image):null,
                ImageName = course.ImageName,
                Modules = course.Modules,
                UsersJoined = course.UsersJoined
            });
            
            var updatedCourse = await _courseService.GetById(courseId);
            await CreateSelectListFromCategories();
            ViewBag.Modules = updatedCourse.Modules;

            if (!ModelState.IsValid)
            {
                await CreateSelectListFromCategories();

                return View(course);
            }

            return RedirectToAction(nameof(Details), new{ courseId });
        }

        private async Task CreateSelectListFromCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            List<SelectListItem> selectCategories = categories.Select(c => new SelectListItem() { 
                Value = c.CategoryId.ToString(), 
                Text = c.CategoryName 
            }).ToList();
            ViewBag.SelectCategories = selectCategories;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchRequest)
        {
            var searchResults = await _courseService.SearchCourses(searchRequest);

            ViewBag.SearchResults = searchResults;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> JoinedCourses()
        {
            var courseUsers = await _courseUserService.GetAllFromUser(Convert.ToInt32(User.FindFirst("UserId").Value));
            List<CourseModel> courses = new List<CourseModel>();

            foreach (var courseUser in courseUsers)
            {
                courses.Add(await _courseService.GetById(courseUser.CourseId));
            }

            ViewBag.Courses = courses;

            return View();
        }

        public async Task<IActionResult> MyCourses()
        {
            List<CourseModel> courses = (await _courseService.GetAllFromAuthor(Convert.ToInt32(User.FindFirst("UserId").Value))).ToList();

            ViewBag.Courses = courses;  

            return View();
        }

        [HttpGet("Courses/{courseId}/Delete")]
        public async Task<IActionResult> Delete(int courseId)
        {
            if (await _customAuthorizationViewService.Authorize(User, courseId))
            {
                await _courseService.Delete(courseId);

                return RedirectToAction(nameof(MyCourses));
            }

            return RedirectToAction(nameof(HomeController.AccessDenied), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
