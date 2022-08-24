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
    public class ExerciseService : IExerciseService
    {
        private readonly UnitOfWork _unitOfWork;
        public ExerciseService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<ExerciseModel> Get(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Lesson, LessonModel>();
            });
            var mapper = config.CreateMapper();

            var exercise = await _unitOfWork.ExerciseRepository.Get(id);

            return mapper.Map<Exercise, ExerciseModel>(exercise);
        }
    }
}
