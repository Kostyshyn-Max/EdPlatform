using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Atempt> AtemptRepository { get; }
        IRepository<Case> CaseRepository { get; }
        IRepository<CodeExercise> CodeExerciseRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<CourseUser> CourseUserRepository { get; }
        IRepository<ExerciseWithAnswer> ExerciseWithAnswerRepository { get; }
        IRepository<IOCase> IOCaseRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<Module> ModuleRepository { get; }
        IRepository<Quiz> QuizRepository { get; }
        IRepository<User> UserRepository { get; }
        void Save();
    }
}
