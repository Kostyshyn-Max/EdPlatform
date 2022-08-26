using EdPlatform.App.AuthorizationPolicy;
using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EdPlatform.App.Services
{
    public class CustomAuthorizationViewService : ICustomAuthorizationViewService
    {
        private readonly ICourseService _courseService;
        private readonly IAuthorizationService _authorizationService;
        public CustomAuthorizationViewService(ICourseService courseService, IAuthorizationService authorizationService)
        {
            _courseService = courseService;
            _authorizationService = authorizationService;
        }

        public async Task<bool> Authorize(ClaimsPrincipal User, int courseId)
        {
            var course = await _courseService.GetById(courseId);
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            return authrorizationResult.Succeeded;
        }

        public async Task<bool> Authorize(ClaimsPrincipal User, CourseModel course)
        {
            var authrorizationResult = await _authorizationService.AuthorizeAsync(User, course, new EditCourseRequirement());

            return authrorizationResult.Succeeded;
        }
    }
}
