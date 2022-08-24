using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using EdPlatform.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork()
        {
            _context = new();
            AttemptRepository = new AttemptRepository(_context);
            CaseRepository = new CaseRepository(_context);
            CodeExerciseRepository = new CodeExerciseRepository(_context);
            CommentRepository = new CommentRepository(_context);
            CourseRepository = new CourseRepository(_context);
            CourseUserRepository = new CourseUserRepository(_context);
            FillExerciseRepository = new FillExerciseRepository(_context);
            IOCaseRepository = new IOCaseRepository(_context);
            LessonRepository = new LessonRepository(_context);
            ModuleRepository = new ModuleRepository(_context);
            QuizRepository = new QuizRepository(_context);
            UserRepository = new UserRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ExerciseRepository = new ExerciseRepository(_context);
        }

        public IAttemptRepository AttemptRepository { get; private set; }

        public ICaseRepository CaseRepository { get; private set; }

        public ICodeExerciseRepository CodeExerciseRepository { get; private set; }

        public ICommentRepository CommentRepository { get; private set; }

        public ICourseRepository CourseRepository { get; private set; }

        public ICourseUserRepository CourseUserRepository { get; private set; }

        public IFillExerciseRepository FillExerciseRepository { get; private set; }

        public IIOCaseRepository IOCaseRepository { get; private set; }

        public ILessonRepository LessonRepository { get; private set; }

        public IModuleRepository ModuleRepository { get; private set; }

        public IQuizRepository QuizRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IExerciseRepository ExerciseRepository { get; set; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
