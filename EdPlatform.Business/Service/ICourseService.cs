using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Service
{
    public interface ICourseService
    {
        Task CreateCourse(CourseModel course);
        Task<IEnumerable<CourseModel>> GetAllFromAuthor(int authorId);
        Task<IEnumerable<CourseModel>> GetAll();
    }
}
