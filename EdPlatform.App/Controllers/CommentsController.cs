using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdPlatform.App.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("Courses/{courseId}/Comments/Post")]
        public IActionResult Post(int courseId)
        {
            return View();
        }

        [HttpPost("Courses/{courseId}/Comments/Post")]
        public async Task<IActionResult> Post(int courseId, CommentModel comment)
        {
            comment.CourseId = courseId;
            comment.UserName = User.FindFirst("Login").Value;
            comment.UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "-1");
            await _commentService.Create(comment);

            return RedirectToAction(nameof(CoursesController.Details), nameof(CoursesController).Replace("Controller", ""), 
                new
                {
                    courseId = courseId,
                }
            );
        }

        [HttpGet("Comments/All")]
        public async Task<IActionResult> All()
        {
            ViewBag.Comments = await _commentService.GetAllByUserId(int.Parse(User.FindFirst("UserId")?.Value ?? "-1"));

            return View();
        }

        [HttpGet("Comments/{commentId}/Edit")]
        public async Task<IActionResult> Edit(int commentId)
        {
            ViewBag.Comment = (await _commentService.GetById(commentId));
        
            return View();
        }

        [HttpPost("Comments/{commentId}/Edit")]
        public async Task<IActionResult> Edit(int commentId, CommentModel comment)
        {
            await _commentService.Edit(comment);

            return RedirectToAction(nameof(All));
        }

        [HttpGet("Comments/{commentId}/Delete")]
        public async Task<IActionResult> Delete(int commentId)
        {
            await _commentService.Delete(commentId);

            return RedirectToAction(nameof(All));
        }
    }
}
