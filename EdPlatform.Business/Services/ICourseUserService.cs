using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICourseUserService
    {
        Task CreateCourseUser(CourseUserModel courseUser);
        Task<CourseUserModel?> Get(CourseUserModel course);
        Task<IEnumerable<CourseUserModel>> GetAllFromUser(int userId);
    }
}
