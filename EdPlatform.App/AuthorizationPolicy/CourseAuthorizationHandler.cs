using EdPlatform.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace EdPlatform.App.AuthorizationPolicy
{
    public class CourseAuthorizationHandler : AuthorizationHandler<EditCourseRequirement, CourseModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditCourseRequirement requirement, CourseModel resource)
        {
            var userId = context.User.Claims.ToList().SingleOrDefault(x => x.Type.Equals("UserId")).Value;
            if (userId != null && int.Parse(userId) == resource.AuthorId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }

    public class EditCourseRequirement : IAuthorizationRequirement { }
}
