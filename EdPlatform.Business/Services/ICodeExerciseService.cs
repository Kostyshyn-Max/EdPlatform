using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface ICodeExerciseService
    {
        Task Create(CodeExerciseModel codeExercise);
        Task<CodeExerciseModel> GetById(int id);
        Task Edit(CodeExerciseModel codeExercise);
        Task<CourseModel> GetCourseById(int courseId);
    }
}
