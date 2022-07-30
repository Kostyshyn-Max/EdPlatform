using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAttemptRepository AttemptRepository { get; }
        ICaseRepository CaseRepository { get; }
        ICodeExerciseRepository CodeExerciseRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseUserRepository CourseUserRepository { get; }
        IFillExerciseRepository FillExerciseRepository { get; }
        IIOCaseRepository IOCaseRepository { get; }
        ILessonRepository LessonRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IQuizRepository QuizRepository { get; }
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task Save();
    }
}
