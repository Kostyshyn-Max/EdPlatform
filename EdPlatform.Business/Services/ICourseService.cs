using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICourseService
    {
        Task CreateCourse(CourseModel course);
        Task<IEnumerable<CourseModel>> GetAllFromAuthor(int authorId);
        Task<IEnumerable<CourseModel>> GetAll();
        Task<CourseModel> GetById(int id);
        Task EditCourse(CourseModel course);
    }
}
