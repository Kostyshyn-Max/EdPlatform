using EdPlatform.Business.Models;
using System.Security.Claims;

namespace EdPlatform.App.Services
{
    public interface ICustomAuthorizationViewService
    {
        public Task<bool> Authorize(ClaimsPrincipal User, int courseId);
        public Task<bool> Authorize(ClaimsPrincipal User, CourseModel course);
    }
}
