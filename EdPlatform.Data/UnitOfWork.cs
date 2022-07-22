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
        private AtemptRepository _atemptRepository;
        private CaseRepository _caseRepository;
        private CodeExerciseRepository _codeExerciseRepository;
        private CommentRepository _commentRepository;
        private CourseRepository _courseRepository;
        private ExerciseWithAnswerRepository _exerciseWithAnswerRepository;
        private CourseUserRepository _courseUserRepository;
        private IOCaseRepository _iOCaseRepository;
        private LessonRepository _lessonRepository;
        private ModuleRepository _moduleRepository;
        private QuizRepository _quizRepository;
        private UserRepository _userRepository;

        private readonly ApplicationDbContext _context;
        public UnitOfWork()
        {
            _context = new();
        }

        public IRepository<Atempt> AtemptRepository
        {
            get
            {
                if (_atemptRepository == null)
                    _atemptRepository = new(_context);
                return _atemptRepository;
            }
        }

        public IRepository<Case> CaseRepository
        {
            get
            {
                if (_caseRepository == null)
                    _caseRepository = new(_context);
                return _caseRepository;
            }
        }

        public IRepository<CodeExercise> CodeExerciseRepository
        {
            get
            {
                if (_codeExerciseRepository == null)
                    _codeExerciseRepository = new(_context);
                return _codeExerciseRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new(_context);
                return _commentRepository;
            }
        }

        public IRepository<Course> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                    _courseRepository = new(_context);
                return _courseRepository;
            }
        }

        public IRepository<CourseUser> CourseUserRepository
        {
            get
            {
                if (_courseUserRepository == null)
                    _courseUserRepository = new(_context);
                return _courseUserRepository;
            }
        }

        public IRepository<ExerciseWithAnswer> ExerciseWithAnswerRepository
        {
            get
            {
                if (_exerciseWithAnswerRepository == null)
                    _exerciseWithAnswerRepository = new(_context);
                return _exerciseWithAnswerRepository;
            }
        }

        public IRepository<IOCase> IOCaseRepository
        {
            get
            {
                if (_iOCaseRepository == null)
                    _iOCaseRepository = new(_context);
                return _iOCaseRepository;
            }
        }

        public IRepository<Lesson> LessonRepository
        {
            get
            {
                if (_lessonRepository == null)
                    _lessonRepository = new(_context);
                return _lessonRepository;
            }
        }

        public IRepository<Module> ModuleRepository
        {
            get
            {
                if (_moduleRepository == null)
                    _moduleRepository = new(_context);
                return _moduleRepository;
            }
        }

        public IRepository<Quiz> QuizRepository
        {
            get
            {
                if (_quizRepository == null)
                    _quizRepository = new(_context);
                return _quizRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new(_context);
                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
