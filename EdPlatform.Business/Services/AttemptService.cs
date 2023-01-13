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
    public class AttemptService : IAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttemptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(AttemptModel attempt, List<bool> results)
        {
            IMapper mapper = CreateAttemptModelToAttemptMapper();

            attempt.IsCompleted = results.Where(x => x.Equals(true)).Count().Equals(results.Count());

            Attempt existingAttempt;

            existingAttempt = (await _unitOfWork.AttemptRepository.Find(x => x.UserId == attempt.UserId && x.ExerciseId == attempt.ExerciseId)).SingleOrDefault();

            if (existingAttempt != null)
            {
                attempt.AttemptId = existingAttempt.AttemptId;
                _unitOfWork.AttemptRepository.Update(mapper.Map<AttemptModel, Attempt>(attempt));
                await _unitOfWork.Save();
            }
            else
            {
                await _unitOfWork.AttemptRepository.Add(mapper.Map<AttemptModel, Attempt>(attempt));
                await _unitOfWork.Save();
            }

        }

        public async Task EditAttempt(AttemptModel attempt)
        {
            IMapper mapper = CreateAttemptModelToAttemptMapper();
                _unitOfWork.AttemptRepository.Update(mapper.Map<AttemptModel, Attempt>(attempt));
                await _unitOfWork.Save();
        }

        public async Task<AttemptModel?> GetFromUserExercise(int userId, int exerciseId)
        {
            IMapper mapper = CreateAttemptToAttemptModelMapper();
            Attempt? attempt;
                attempt = (await _unitOfWork.AttemptRepository.Find(x => x.UserId == userId && x.ExerciseId == exerciseId)).SingleOrDefault();

            return mapper.Map<Attempt, AttemptModel>(attempt);
        }

        public async Task<int> GetNotSolvedExerciseId(IEnumerable<ExerciseModel> exercises, int userId)
        {
            IMapper mapper = CreateAttemptToAttemptModelMapper();
            List<AttemptModel?> attempts = await GetAttemptsList(exercises, userId, mapper);

            for (int i = 0; i < exercises.Count(); i++)
            {
                if (attempts[i] == null)
                {
                    return exercises.ElementAt(i).ExerciseId;
                }
            }

            return exercises.ElementAt(exercises.Count() - 1).ExerciseId;
        }

        public async Task<List<AttemptModel?>> GetAllAttemptsFromExercises(IEnumerable<ExerciseModel> exercises, int userId)
        {
            IMapper mapper = CreateAttemptToAttemptModelMapper();
            List<AttemptModel?> attempts = await GetAttemptsList(exercises, userId, mapper);

            return attempts;
        }

        private async Task<List<AttemptModel?>> GetAttemptsList(IEnumerable<ExerciseModel> exercises, int userId, IMapper mapper)
        {
            exercises = exercises.OrderBy(x => x.Order).ToList();
            List<AttemptModel?> attempts = new List<AttemptModel?>();

            foreach (var exercise in exercises)
            {
                var attempt = (await _unitOfWork.AttemptRepository.Find(x => x.ExerciseId == exercise.ExerciseId && x.UserId == userId)).SingleOrDefault();

                if (attempt != null)
                    attempts.Add(mapper.Map<Attempt, AttemptModel>(attempt));
                else
                    attempts.Add(null);
            }

            return attempts;
        }

        private static IMapper CreateAttemptToAttemptModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Attempt, AttemptModel>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Lesson, LessonModel>();
                cfg.CreateMap<Module, ModuleModel>();
                cfg.CreateMap<Course, CourseModel>();
                cfg.CreateMap<Category, CategoryModel>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        private IMapper CreateAttemptModelToAttemptMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AttemptModel, Attempt>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
