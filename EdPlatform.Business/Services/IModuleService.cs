using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface IModuleService
    {
        Task CreateModule(ModuleModel module);
        Task<IEnumerable<ModuleModel>> GetAllModulesFromCourse(int courseId);
        Task EditModule(ModuleModel module);
        Task<ModuleModel> GetById(int moduleId);
        Task<CourseModel> GetCourseById(int courseId);
    }
}
