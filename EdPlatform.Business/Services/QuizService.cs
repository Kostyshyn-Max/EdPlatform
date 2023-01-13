using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuizService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(QuizModel quizExercise)
        {
            IMapper mapper = CreateQuizModelToQuizMapper();

            await _unitOfWork.QuizRepository.Add(mapper.Map<QuizModel, Quiz>(quizExercise));
            await _unitOfWork.Save();
        }

        public async Task<QuizModel> Get(int quizExerciseId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Quiz, QuizModel>();
                cfg.CreateMap<Case, CaseModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
            });
            var mapper = config.CreateMapper();

            var quiz = mapper.Map<Quiz, QuizModel>(await _unitOfWork.QuizRepository.Get(quizExerciseId));
            return quiz;
        }

        public async Task Edit(QuizModel quiz)
        {
            IMapper mapper = CreateQuizModelToQuizMapper();

            _unitOfWork.QuizRepository.Update(mapper.Map<QuizModel, Quiz>(quiz));
            await _unitOfWork.Save();
        }

        private static IMapper CreateQuizModelToQuizMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuizModel, Quiz>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
