using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class QuizService : IQuizService
    {
        private readonly UnitOfWork _unitOfWork;
        public QuizService()
        {
            _unitOfWork = new();
        }

        public async Task Create(QuizModel quizExercise)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuizModel, Quiz>();
            });
            var mapper = config.CreateMapper();

            await _unitOfWork.QuizRepository.Add(mapper.Map<QuizModel, Quiz>(quizExercise));
            await _unitOfWork.Save();
        }
    }
}
