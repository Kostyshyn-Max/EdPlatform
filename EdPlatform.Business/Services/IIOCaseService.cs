using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface IIOCaseService
    {
        Task Create(IOCaseModel iOCase);
        Task<IOCaseModel> GetById(int id);
        Task<IEnumerable<IOCaseModel>> GetFromExercise(int exerciseId);
        Task Edit (IOCaseModel iOCase);
        Task<CourseModel> GetCourseById(int courseId);
    }
}
